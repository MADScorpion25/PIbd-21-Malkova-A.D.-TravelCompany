using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;
using TravelCompanyDatabaseImplement.Models;

namespace TravelCompanyDatabaseImplement.Implements
{
    public class TravelStorage : ITravelStorage
    {
        public List<TravelViewModel> GetFullList()
        {
            var context = new TravelCompanyDatabase();
            return context.Travels.Include(rec => rec.TravelConditions).ThenInclude(rec => rec.Condition).ToList().Select(rec => new TravelViewModel
            {
                Id = rec.Id,
                TravelName = rec.TravelName,
                Price = rec.Price,
                TravelConditions = rec.TravelConditions
                .ToDictionary(recTC => recTC.ConditionId, recPC =>
               (recPC.Condition?.ConditionName, recPC.Count))
            })
               .ToList();
        }

        public List<TravelViewModel> GetFilteredList(TravelBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var context = new TravelCompanyDatabase();
            return context.Travels.Include(rec => rec.TravelConditions).ThenInclude(rec => rec.Condition).
                Where(rec => rec.TravelName.Contains(model.TravelName)).ToList().Select(rec => new TravelViewModel
                {
                    Id = rec.Id,
                    TravelName = rec.TravelName,
                    Price = rec.Price,
                    TravelConditions = rec.TravelConditions
                .ToDictionary(recTC => recTC.ConditionId, recPC =>
               (recPC.Condition?.ConditionName, recPC.Count))
                })
               .ToList();
        }

        public TravelViewModel GetElement(TravelBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var context = new TravelCompanyDatabase();
             var travel = context.Travels.Include(rec => rec.TravelConditions).ThenInclude(rec => rec.Condition)
                .FirstOrDefault(rec => rec.TravelName == model.TravelName || rec.Id == model.Id);
            return travel != null ?
            new TravelViewModel
            {
                Id = travel.Id,
                TravelName = travel.TravelName,
                Price = travel.Price,
                TravelConditions = travel.TravelConditions
            .ToDictionary(recPC => recPC.ConditionId, recTC =>
           (recTC.Condition?.ConditionName, recTC.Count))
            } :
           null;
        }

        public void Insert(TravelBindingModel model)
        {
            var context = new TravelCompanyDatabase();
            var transaction = context.Database.BeginTransaction();
            try
            {
                Travel t = new Travel
                {
                    TravelName = model.TravelName,
                    Price = model.Price
                };
                context.Travels.Add(t);
                context.SaveChanges();
                CreateModel(model, t, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(TravelBindingModel model)
        {
            var context = new TravelCompanyDatabase();
            var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Travels.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(TravelBindingModel model)
        {
            var context = new TravelCompanyDatabase();
            Travel element = context.Travels.FirstOrDefault(rec => rec.Id ==
               model.Id);
            if (element != null)
            {
                context.Travels.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private Travel CreateModel(TravelBindingModel model, Travel travel, TravelCompanyDatabase context)
        {
            if (model.Id.HasValue)
            {
                var travelConditions = context.TravelConditions.Where(rec => rec.TravelId == model.Id.Value).ToList();
                context.TravelConditions.RemoveRange(travelConditions.Where(rec => !model.TravelConditions.ContainsKey(rec.ConditionId)).ToList());
                context.SaveChanges();
                foreach (var updateCondition in travelConditions)
                {
                    updateCondition.Count =
                    model.TravelConditions[updateCondition.ConditionId].Item2;
                    model.TravelConditions.Remove(updateCondition.ConditionId);
                }
                context.SaveChanges();
            }
            foreach (var tc in model.TravelConditions)
            {
                context.TravelConditions.Add(new TravelCondition
                {
                    TravelId = travel.Id,
                    ConditionId = tc.Key,
                    Count = tc.Value.Item2
                });
                context.SaveChanges();
            }
            return travel;
        }
    }
}

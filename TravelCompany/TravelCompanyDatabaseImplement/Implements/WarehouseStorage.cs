using System;
using System.Collections.Generic;
using System.Linq;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;
using Microsoft.EntityFrameworkCore;
using TravelCompanyDatabaseImplement.Models;

namespace TravelCompanyDatabaseImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {

        public List<WarehouseViewModel> GetFullList()
        {
            using var context = new TravelCompanyDatabase();
            return context.Warehouses.Include(rec => rec.WarehouseConditions).ThenInclude(rec => rec.Condition).ToList().Select(CreateModel).ToList();
        }
        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelCompanyDatabase();
            return context.Warehouses.Include(rec => rec.WarehouseConditions).ThenInclude(rec => rec.Condition).Where(rec => rec.WarehouseName.Contains(model.WarehouseName)).ToList().Select(CreateModel).ToList();
        }
        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelCompanyDatabase();
            var warehouse = context.Warehouses.Include(rec => rec.WarehouseConditions).ThenInclude(rec => rec.Condition).FirstOrDefault(rec => rec.WarehouseName == model.WarehouseName || rec.Id == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }
        public void Insert(WarehouseBindingModel model)
        {
            using var context = new TravelCompanyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Warehouse warehouse = new Warehouse()
                {
                    WarehouseName = model.WarehouseName,
                    ResponsibleFullName = model.ResponsibleFullName,
                    DateCreate = model.CreateDate
                };
                context.Warehouses.Add(warehouse);
                context.SaveChanges();
                CreateModel(model, warehouse, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(WarehouseBindingModel model)
        {
            using var context = new TravelCompanyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
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
        public void Delete(WarehouseBindingModel model)
        {
            using var context = new TravelCompanyDatabase();
            Warehouse element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Warehouses.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse, TravelCompanyDatabase context)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ResponsibleFullName = model.ResponsibleFullName;
            warehouse.DateCreate = model.CreateDate;
            if (model.Id.HasValue)
            {
                var warehouseConditions = context.WarehouseConditions.Where(rec => rec.WarehouseId == model.Id.Value).ToList();
                context.WarehouseConditions.RemoveRange(warehouseConditions.Where(rec => !model.WarehouseConditions.ContainsKey(rec.ConditionId)).ToList());
                context.SaveChanges();
                foreach (var updateCondition in warehouseConditions)
                {
                    updateCondition.Count = model.WarehouseConditions[updateCondition.ConditionId].Item2;
                    model.WarehouseConditions.Remove(updateCondition.ConditionId);
                }
                context.SaveChanges();
            }
            foreach (var pc in model.WarehouseConditions)
            {
                context.WarehouseConditions.Add(new WarehouseCondition
                {
                    WarehouseId = warehouse.Id,
                    ConditionId = pc.Key,
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            return warehouse;
        }
        private static WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                ResponsibleFullName = warehouse.ResponsibleFullName,
                CreateDate = warehouse.DateCreate,
                WarehouseConditions = warehouse.WarehouseConditions.ToDictionary(recPC => recPC.ConditionId, recPC => (recPC.Condition?.ConditionName, recPC.Count))
            };
        }
        public bool TakeConditionFromWarehouse(Dictionary<int, (string, int)> conditions, int orderCount)
        {
            var context = new TravelCompanyDatabase();
            var transaction = context.Database.BeginTransaction();
            try
            {
                foreach (var warehouseCondition in conditions)
                {
                    int count = warehouseCondition.Value.Item2 * orderCount;
                    IEnumerable<WarehouseCondition> warehouseConditions = context.WarehouseConditions
                        .Where(warehouse => warehouse.ConditionId == warehouseCondition.Key);

                    int totalCount = warehouseConditions.Sum(warehouse => warehouse.Count);
                    foreach (var component in warehouseConditions)
                    {
                        if (component.Count <= count)
                        {
                            count -= component.Count;
                            context.WarehouseConditions.Remove(component);
                            context.SaveChanges();
                        }
                        else
                        {
                            component.Count -= count;
                            context.SaveChanges();
                            count = 0;
                        }
                        if (count == 0)
                        {
                            break;
                        }
                    }
                    if (count != 0)
                    {
                        throw new Exception("Недостаточно условий для передания заказа в работу");
                    }
                }
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
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

        public void Delete(WarehouseBindingModel model)
        {
            var context = new TravelCompanyDatabase();
            var warehouse = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);

            if (warehouse == null)
            {
                throw new Exception("Склад не найден");
            }

            context.Warehouses.Remove(warehouse);
            context.SaveChanges();
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var context = new TravelCompanyDatabase();
            var warehouse = context.Warehouses
                    .Include(rec => rec.WarehouseConditions)
                    .ThenInclude(rec => rec.Condition)
                    .FirstOrDefault(rec => rec.WarehouseName == model.WarehouseName || rec.Id == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var context = new TravelCompanyDatabase();
            return context.Warehouses
                .Include(rec => rec.WarehouseConditions)
                .ThenInclude(rec => rec.Condition)
                .Where(rec => rec.WarehouseName.Contains(model.WarehouseName))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<WarehouseViewModel> GetFullList()
        {
            var context = new TravelCompanyDatabase();
            return context.Warehouses
                .Include(rec => rec.WarehouseConditions)
                .ThenInclude(rec => rec.Condition)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(WarehouseBindingModel model)
        {
            var context = new TravelCompanyDatabase();
            var transaction = context.Database.BeginTransaction();
            try
            {
                CreateModel(model, new Warehouse(), context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
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

        public void Update(WarehouseBindingModel model)
        {
            var context = new TravelCompanyDatabase();
            var transaction = context.Database.BeginTransaction();
            try
            {
                var warehouse = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);

                if (warehouse == null)
                {
                    throw new Exception("Склад не найден");
                }

                CreateModel(model, warehouse, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse, TravelCompanyDatabase context)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ResponsibleFullName = model.ResponsibleFullName;

            if (warehouse.Id == 0)
            {
                warehouse.DateCreate = DateTime.Now;
                context.Warehouses.Add(warehouse);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var WarehouseConditions = context.WarehouseConditions
                    .Where(rec => rec.WarehouseId == model.Id.Value)
                    .ToList();

                context.WarehouseConditions.RemoveRange(WarehouseConditions
                    .Where(rec => !model.WarehouseConditions.ContainsKey(rec.ConditionId))
                    .ToList());
                context.SaveChanges();

                foreach (var updateCondition in WarehouseConditions)
                {
                    updateCondition.Count = model.WarehouseConditions[updateCondition.ConditionId].Item2;
                    model.WarehouseConditions.Remove(updateCondition.ConditionId);
                }
                context.SaveChanges();
            }


            foreach (var WarehouseCondition in model.WarehouseConditions)
            {
                context.WarehouseConditions.Add(new WarehouseCondition
                {
                    WarehouseId = warehouse.Id,
                    ConditionId = WarehouseCondition.Key,
                    Count = WarehouseCondition.Value.Item2
                });
                context.SaveChanges();
            }

            return warehouse;
        }
        private WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                ResponsibleFullName = warehouse.ResponsibleFullName,
                CreateDate = warehouse.DateCreate,
                WarehouseConditions = warehouse.WarehouseConditions
                        .ToDictionary(recWarehouseConditions => recWarehouseConditions.ConditionId,
                         recWarehouseConditions => (recWarehouseConditions.Condition?.ConditionName,
                         recWarehouseConditions.Count))
            };
        }
    }
}
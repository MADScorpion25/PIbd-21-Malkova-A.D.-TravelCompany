using System;
using System.Collections.Generic;
using System.Linq;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;
using TravelCompanyListImplement.Models;

namespace TravelCompanyListImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly DataListSingleton source;
        public WarehouseStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public void Delete(WarehouseBindingModel model)
        {
            for (int i = 0; i < source.Warehouses.Count; ++i)
            {
                if (source.Warehouses[i].Id == model.Id)
                {
                    source.Warehouses.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Id == model.Id ||
                    warehouse.WarehouseName == model.WarehouseName)
                {
                    return CreateModel(warehouse);
                }
            }
            return null;
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<WarehouseViewModel>();
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.WarehouseName.Contains(model.WarehouseName))
                {
                    result.Add(CreateModel(warehouse));
                }
            }
            return result;
        }

        public List<WarehouseViewModel> GetFullList()
        {
            var result = new List<WarehouseViewModel>();
            foreach (var condition in source.Warehouses)
            {
                result.Add(CreateModel(condition));
            }
            return result;
        }

        public void Insert(WarehouseBindingModel model)
        {
            var tempWarehouse = new Warehouse { Id = 1, WarehouseConditions = new Dictionary<int, int>() };
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Id >= tempWarehouse.Id)
                {
                    tempWarehouse.Id = warehouse.Id + 1;
                }
            }
            source.Warehouses.Add(CreateModel(model, tempWarehouse));
        }

        public bool TakeConditionFromWarehouse(Dictionary<int, (string, int)> conditions, int orderCount)
        {
            throw new NotImplementedException();
        }

        public void Update(WarehouseBindingModel model)
        {
            Warehouse tempWarehouse = null;
            foreach (var warehouse in source.Warehouses)
            {
                if (warehouse.Id == model.Id)
                {
                    tempWarehouse = warehouse;
                }
            }
            if (tempWarehouse == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempWarehouse);
        }
        private WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            var warehouseConditions = new Dictionary<int, (string, int)>();
            foreach (var warehouseCondition in warehouse.WarehouseConditions)
            {
                string conditionName = string.Empty;
                foreach (var condition in source.Conditions)
                {
                    if (warehouseCondition.Key == condition.Id)
                    {
                        conditionName = condition.ConditionName;
                        break;
                    }
                }
                warehouseConditions.Add(warehouseCondition.Key, (conditionName, warehouseCondition.Value));
            }
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                ResponsibleFullName = warehouse.ResponsibleFullName,
                CreateDate = warehouse.CreateDate,
                WarehouseConditions = warehouseConditions
            };
        }
        private Warehouse CreateModel(WarehouseBindingModel model,
            Warehouse warehouse)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ResponsibleFullName = model.ResponsibleFullName;
            warehouse.CreateDate = model.CreateDate;
            foreach (var key in warehouse.WarehouseConditions.Keys.ToList())
            {
                if (!model.WarehouseConditions.ContainsKey(key))
                {
                    warehouse.WarehouseConditions.Remove(key);
                }
            }
            foreach (var condition in model.WarehouseConditions)
            {
                if (warehouse.WarehouseConditions.ContainsKey(condition.Key))
                {
                    warehouse.WarehouseConditions[condition.Key] = model.WarehouseConditions[condition.Key].Item2;
                }
                else
                {
                    warehouse.WarehouseConditions.Add(condition.Key, model.WarehouseConditions[condition.Key].Item2);
                }
            }
            return warehouse;
        }
    }
}

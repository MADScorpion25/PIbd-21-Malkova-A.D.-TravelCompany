using System;
using System.Collections.Generic;
using System.Linq;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;
using TravelCompanyFileImplement.Models;

namespace TravelCompanyFileImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly FileDataListSingleton source;
        public WarehouseStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void Delete(WarehouseBindingModel model)
        {
            Warehouse element = source.Warehouses
                .FirstOrDefault(rec => rec.Id == model.Id);
            if(element != null)
            {
                source.Warehouses.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var warehouse = source.Warehouses
                .FirstOrDefault(rec => rec.Id == model.Id || rec.WarehouseName == model.WarehouseName);
            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Warehouses
                .Where(rec => rec.WarehouseName.Contains(model.WarehouseName))
                .Select(CreateModel)
                .ToList();
        }

        public List<WarehouseViewModel> GetFullList()
        {
            return source.Warehouses
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(WarehouseBindingModel model)
        {
            int maxId = source.Warehouses.Count > 0 ? source.Conditions.Max(rec => rec.Id) : 0;
            var element = new Warehouse
            {
                Id = maxId + 1,
                WarehouseConditions = new Dictionary<int, int>()
            };
            source.Warehouses.Add(CreateModel(model, element));
        }

        public bool TakeConditionFromWarehouse(Dictionary<int, (string, int)> conditions, int orderCount)
        {
            foreach(var condition in conditions)
            {
                int count = source.Warehouses
                    .Where(travel => travel.WarehouseConditions.ContainsKey(condition.Key)).Sum(travel => travel.WarehouseConditions[condition.Key]);
                if (count < condition.Value.Item2 * orderCount)
                {
                    return false;
                }
            }
            foreach (var condition in conditions)
            {
                int reqCount = condition.Value.Item2 * orderCount;
                foreach (var warehouse in source.Warehouses)
                {
                    var warehouseCond = warehouse.WarehouseConditions;
                    if (!warehouseCond.ContainsKey(condition.Key))
                    {
                        continue;
                    }
                    if(warehouseCond[condition.Key] > reqCount)
                    {
                        warehouseCond[condition.Key] -= reqCount;
                        break;
                    }
                    else if(warehouseCond[condition.Key] <= reqCount)
                    {
                        reqCount -= warehouseCond[condition.Key];
                        warehouseCond.Remove(condition.Key);
                    }
                    if(reqCount == 0)
                    {
                        break;
                    }
                }
            }
            return true;
        }

        public void Update(WarehouseBindingModel model)
        {
            var element = source.Warehouses
               .FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
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
                WarehouseConditions = warehouse.WarehouseConditions.ToDictionary(recPC => recPC.Key, recPC => (source.Conditions.FirstOrDefault(recC => recC.Id == recPC.Key)?.ConditionName, recPC.Value))
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

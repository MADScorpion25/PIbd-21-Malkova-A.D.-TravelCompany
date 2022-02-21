using System;
using System.Collections.Generic;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.BusinessLogicsContracts;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyBusinessLogic.BusinessLogics
{
    public class WarehouseLogic : IWarehouseLogic
    {
        public IWarehouseStorage _warehouseStorage;
        public IConditionStorage _componentStorage;
        public WarehouseLogic(IWarehouseStorage _warehouseStorage, IConditionStorage _componentStorage)
        {
            this._warehouseStorage = _warehouseStorage;
            this._componentStorage = _componentStorage;
        }
        public void AddCondition(WarehouseBindingModel model, int componentId, int count)
        {
            var warehouse = _warehouseStorage.GetElement(new WarehouseBindingModel { Id = model.Id });
            if (warehouse == null)
            {
                throw new Exception("Склад не найден");
            }
            var component = _componentStorage.GetElement(new ConditionBindingModel { Id = componentId });
            if (component == null)
            {
                throw new Exception("Компонент не найден");
            }
            if (warehouse.WarehouseConditions.ContainsKey(componentId))
            {
                warehouse.WarehouseConditions[componentId] = (component.ConditionName, warehouse.WarehouseConditions[componentId].Item2 + count);
            }
            else
            {
                warehouse.WarehouseConditions.Add(componentId, (component.ConditionName, count));
            }
            _warehouseStorage.Update(new WarehouseBindingModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                ResponsibleFullName = warehouse.ResponsibleFullName,
                CreateDate = warehouse.CreateDate,
                WarehouseConditions = warehouse.WarehouseConditions
            });
        }

        public void CreateOrUpdate(WarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                WarehouseName = model.WarehouseName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            if (model.Id.HasValue)
            {
                _warehouseStorage.Update(model);
            }
            else
            {
                _warehouseStorage.Insert(model);
            }
        }

        public void Delete(WarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _warehouseStorage.Delete(model);
        }

        public List<WarehouseViewModel> Read(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return _warehouseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<WarehouseViewModel> { _warehouseStorage.GetElement(model) };
            }
            return _warehouseStorage.GetFilteredList(model);
        }
    }
}

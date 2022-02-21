using System;
using System.Collections.Generic;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.BusinessLogicsContracts;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyBusinessLogic.BusinessLogics
{
    public class ConditionLogic : IConditionLogic
    {
        private readonly IConditionStorage _conditionStorage;
        public ConditionLogic(IConditionStorage ConditionStorage)
        {
            _conditionStorage = ConditionStorage;
        }
        public List<ConditionViewModel> Read(ConditionBindingModel model)
        {
            if (model == null)
            {
                return _conditionStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ConditionViewModel> { _conditionStorage.GetElement(model)
};
            }
            return _conditionStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(ConditionBindingModel model)
        {
            var element = _conditionStorage.GetElement(new ConditionBindingModel { ConditionName = model.ConditionName });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            if (model.Id.HasValue)
            {
                _conditionStorage.Update(model);
            }
            else
            {
                _conditionStorage.Insert(model);
            }
        }
        public void Delete(ConditionBindingModel model)
        {
            var element = _conditionStorage.GetElement(new ConditionBindingModel
            {
                Id =
           model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _conditionStorage.Delete(model);
        }
    }
}
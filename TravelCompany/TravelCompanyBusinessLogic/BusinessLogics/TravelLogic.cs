using System;
using System.Collections.Generic;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.BusinessLogicsContracts;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyBusinessLogic.BusinessLogics
{
    public class TravelLogic : ITravelLogic
    {
        private readonly ITravelStorage _travelStorage;
        public TravelLogic(ITravelStorage travelStorage)
        {
            _travelStorage = travelStorage;
        }
        public void CreateOrUpdate(TravelBindingModel model)
        {
            var element = _travelStorage.GetElement(new TravelBindingModel
            {
                TravelName = model.TravelName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            if (model.Id.HasValue)
            {
                _travelStorage.Update(model);
            }
            else
            {
                _travelStorage.Insert(model);
            }
        }

        public void Delete(TravelBindingModel model)
        {
            var element = _travelStorage.GetElement(new TravelBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _travelStorage.Delete(model);
        }

        public List<TravelViewModel> Read(TravelBindingModel model)
        {
            if (model == null)
            {
                return _travelStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TravelViewModel> { _travelStorage.GetElement(model) };
            }
            return _travelStorage.GetFilteredList(model);
        }
    }
}
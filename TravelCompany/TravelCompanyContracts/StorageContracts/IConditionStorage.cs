using System.Collections.Generic;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyContracts.StorageContracts
{
    public interface IConditionStorage
    {
        List<ConditionViewModel> GetFullList();
        List<ConditionViewModel> GetFilteredList(ConditionBindingModel model);
        ConditionViewModel GetElement(ConditionBindingModel model);
        void Insert(ConditionBindingModel model);
        void Update(ConditionBindingModel model);
        void Delete(ConditionBindingModel model);
    }
}

using System.Collections.Generic;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyContracts.BusinessLogicsContracts
{
    public interface IConditionLogic
    {
        List<ConditionViewModel> Read(ConditionBindingModel model);
        void CreateOrUpdate(ConditionBindingModel model);
        void Delete(ConditionBindingModel model);
    }
}

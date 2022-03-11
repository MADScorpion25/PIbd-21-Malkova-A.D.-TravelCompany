using System.Collections.Generic;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyContracts.BusinessLogicsContracts
{
    public interface ITravelLogic
    {
        List<TravelViewModel> Read(TravelBindingModel model);
        void CreateOrUpdate(TravelBindingModel model);
        void Delete(TravelBindingModel model);
    }
}

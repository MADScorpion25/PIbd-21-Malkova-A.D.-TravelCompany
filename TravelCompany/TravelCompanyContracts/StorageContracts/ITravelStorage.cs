using System.Collections.Generic;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyContracts.StorageContracts
{
    public interface ITravelStorage
    {
        List<TravelViewModel> GetFullList();
        List<TravelViewModel> GetFilteredList(TravelBindingModel model);
        TravelViewModel GetElement(TravelBindingModel model);
        void Insert(TravelBindingModel model);
        void Update(TravelBindingModel model);
        void Delete(TravelBindingModel model);
    }
}

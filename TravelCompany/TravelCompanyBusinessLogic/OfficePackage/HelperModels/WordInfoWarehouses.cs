using System.Collections.Generic;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfoWarehouses : WordInfo
    {
        public List<WarehouseViewModel> Warehouses { get; set; }
    }
}

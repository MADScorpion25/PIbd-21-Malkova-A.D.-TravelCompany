using System.Collections.Generic;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfoWarehouses
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<WarehouseViewModel> Warehouses { get; set; }
    }
}

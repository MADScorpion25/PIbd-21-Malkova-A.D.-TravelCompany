using System.Collections.Generic;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfoWarehouses
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportWarehouseConditionViewModel> WarehouseConditions { get; set; }
    }
}

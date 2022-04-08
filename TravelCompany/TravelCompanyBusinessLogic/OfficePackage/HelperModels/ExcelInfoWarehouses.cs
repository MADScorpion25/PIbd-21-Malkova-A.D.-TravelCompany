using System.Collections.Generic;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfoWarehouses : ExcelInfo
    {
        public List<ReportWarehouseConditionViewModel> WarehouseConditions { get; set; }
    }
}

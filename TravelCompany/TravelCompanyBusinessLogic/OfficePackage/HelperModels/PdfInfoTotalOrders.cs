using System.Collections.Generic;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfoTotalOrders : PdfInfo
    {
        public List<ReportTotalOrdersViewModel> TotalOrders { get; set; }
    }
}

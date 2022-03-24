using System;

namespace TravelCompanyContracts.ViewModels
{
    public class ReportTotalOrdersViewModel
    {
        public DateTime DateCreate { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalSum { get; set; }
    }
}

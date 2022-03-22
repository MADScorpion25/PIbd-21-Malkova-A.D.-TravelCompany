using System;
using System.Collections.Generic;

namespace TravelCompanyContracts.ViewModels
{
    public class ReportTravelConditionViewModel
    {
        public string ConditionName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Travels { get; set; }
    }
}

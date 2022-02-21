using System.Collections.Generic;
using System.ComponentModel;

namespace TravelCompanyContracts.ViewModels
{
    public class TravelViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название туристической путевки")]
        public string TravelName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> TravelConditions { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using TravelCompanyContracts.Attributes;

namespace TravelCompanyContracts.ViewModels
{
    public class TravelViewModel
    {
        public int Id { get; set; }
        [Column(title: "Путевка", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string TravelName { get; set; }

        [Column(title: "Цена", width: 100, dateFormat: "C2")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> TravelConditions { get; set; }
    }
}

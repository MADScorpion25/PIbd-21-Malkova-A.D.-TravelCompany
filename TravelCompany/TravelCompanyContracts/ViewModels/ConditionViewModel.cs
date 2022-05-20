using System.ComponentModel;
using TravelCompanyContracts.Attributes;

namespace TravelCompanyContracts.ViewModels
{
    public class ConditionViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }

        [Column(title: "Название условия поездки", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ConditionName { get; set; }
    }
}

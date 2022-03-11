using System.ComponentModel;

namespace TravelCompanyContracts.ViewModels
{
    public class ConditionViewModel
    {
        public int Id { get; set; }
        [DisplayName("Условие туристической путевки")]
        public string ConditionName { get; set; }
    }
}

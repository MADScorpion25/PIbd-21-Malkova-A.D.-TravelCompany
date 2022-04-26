using System;
using System.Collections.Generic;
using System.Text;

namespace TravelCompanyContracts.BindingModels
{
    public class WarehouseConditionsBindingModel
    {
        public int WarehouseId { get; set; }

        public int ConditionId { get; set; }

        public int Count { get; set; }
    }
}

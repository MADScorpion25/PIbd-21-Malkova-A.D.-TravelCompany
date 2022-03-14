using System;
using System.Collections.Generic;

namespace TravelCompanyContracts.BindingModels
{
    public class WarehouseBindingModel
    {
        public int? Id { get; set; }
        public string WarehouseName { get; set; }
        public string ResponsibleFullName { get; set; }
        public DateTime CreateDate { get; set; }
        public Dictionary<int, (string, int)> WarehouseConditions { get; set; }
    }
}

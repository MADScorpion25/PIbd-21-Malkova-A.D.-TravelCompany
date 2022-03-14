using System;
using System.Collections.Generic;

namespace TravelCompanyListImplement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string WarehouseName { get; set; }
        public string ResponsibleFullName { get; set; }
        public DateTime CreateDate { get; set; }
        public Dictionary<int, int> WarehouseConditions { get; set; }
    }
}

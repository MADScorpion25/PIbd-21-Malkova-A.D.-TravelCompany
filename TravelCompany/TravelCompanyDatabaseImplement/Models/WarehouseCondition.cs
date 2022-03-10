using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelCompanyDatabaseImplement.Models
{
    public class WarehouseCondition
    {
        public int Id { get; set; }

        public int ConditionId { get; set; }

        public int WarehouseId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Condition Condition { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}

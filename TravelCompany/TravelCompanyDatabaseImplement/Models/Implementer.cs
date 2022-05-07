using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelCompanyDatabaseImplement.Models
{
    public class Implementer
    {
        public int Id { get; set; }
        public string ImplementerFIO { get; set; }
        public int WorkingTime { get; set; }
        public int PauseTime { get; set; }

        [ForeignKey("ImplementerId")]
        public List<Order> Orders { get; set; }
    }
}

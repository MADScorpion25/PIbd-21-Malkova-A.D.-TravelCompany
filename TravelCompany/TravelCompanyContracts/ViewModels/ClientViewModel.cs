using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelCompanyContracts.Attributes;

namespace TravelCompanyContracts.ViewModels
{
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }
        [Column(title: "Клиент", width: 150)]
        public string ClientFIO { get; set; }
        [Column(title: "Логин", width: 100)]
        public string Login { get; set; }
        [Column(title: "Пароль", width: 100)]
        public string Password { get; set; }
    }
}

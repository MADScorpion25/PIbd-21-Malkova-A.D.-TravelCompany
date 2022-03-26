using System;
using System.ComponentModel;
using TravelCompanyContracts.Enums;

namespace TravelCompanyContracts.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int TravelId { get; set; }
        public int ClientId { get; set; }
        [DisplayName("ФИО клиента")]
        public string ClientFIO { get; set; }
        [DisplayName("Туристическая путевка")]
        public string TravelName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}

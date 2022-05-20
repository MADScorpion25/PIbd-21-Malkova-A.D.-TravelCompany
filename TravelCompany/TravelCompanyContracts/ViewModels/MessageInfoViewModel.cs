using System;
using TravelCompanyContracts.Attributes;
using TravelCompanyContracts.Enums;

namespace TravelCompanyContracts.ViewModels
{
    public class MessageInfoViewModel
    {
        public string MessageId { get; set; }
        [Column(title: "Отправитель", width: 100)]
        public string SenderName { get; set; }
        [Column(title: "Дата письма", width: 50, dateFormat: "d")]
        public DateTime DateDelivery { get; set; }
        [Column(title: "Заголовок", width: 150)]
        public string Subject { get; set; }
        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Body { get; set; }
        [Column(title: "Статус", width: 50)]
        public MessageStatus MessageStatus { get; set; }
        [Column(title: "Ответ", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ReplyText { get; set; }
    }
}

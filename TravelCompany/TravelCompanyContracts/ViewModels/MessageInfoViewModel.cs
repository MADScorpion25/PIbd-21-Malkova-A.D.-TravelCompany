using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using TravelCompanyContracts.Enums;

namespace TravelCompanyContracts.ViewModels
{
    public class MessageInfoViewModel
    {
        public string MessageId { get; set; }
        [DataMember]
        [DisplayName("Отправитель")]
        public string SenderName { get; set; }
        [DataMember]
        [DisplayName("Дата письма")]
        public DateTime DateDelivery { get; set; }
        [DataMember]
        [DisplayName("Заголовок")]
        public string Subject { get; set; }
        [DataMember]
        [DisplayName("Текст")]
        public string Body { get; set; }
        [DataMember]
        [DisplayName("Статус")]
        public MessageStatus MessageStatus { get; set; }
        [DataMember]
        [DisplayName("Ответ")]
        public string ReplyText { get; set; }
    }
}

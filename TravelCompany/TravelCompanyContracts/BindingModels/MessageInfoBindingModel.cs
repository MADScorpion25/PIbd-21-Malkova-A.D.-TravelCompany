using System;
using System.Collections.Generic;
using System.Text;
using TravelCompanyContracts.Enums;

namespace TravelCompanyContracts.BindingModels
{
    public class MessageInfoBindingModel
    {
        public int? ClientId { get; set; }
        public string MessageId { get; set; }
        public string FromMailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateDelivery { get; set; }
        public string ReplyText { get; set; }
        public MessageStatus MessageStatus { get; set; }
        public int? ToSkip { get; set; }
        public int? ToTake { get; set; }
    }
}

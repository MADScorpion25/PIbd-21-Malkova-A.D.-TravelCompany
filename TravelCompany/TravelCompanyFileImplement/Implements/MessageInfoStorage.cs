using System;
using System.Collections.Generic;
using System.Linq;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;
using TravelCompanyFileImplement.Models;

namespace TravelCompanyFileImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly FileDataListSingleton source;

        public MessageInfoStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<MessageInfoViewModel> GetFullList()
        {
            return source.MessageInfoes
            .Select(CreateModel)
            .ToList();
        }
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.MessageInfoes.Where(rec => model.ClientId.HasValue ?
                 (rec.ClientId == model.ClientId)
                 :
                 (model.ToSkip.HasValue && model.ToTake.HasValue || rec.DateDelivery.Date == model.DateDelivery.Date))
                 .Skip(model.ToSkip ?? 0)
                 .Take(model.ToTake ?? source.MessageInfoes.Count())
                 .Select(CreateModel)
                 .ToList();
        }
        public void Insert(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            source.MessageInfoes.Add(CreateModel(model, new MessageInfo()));
        }

        private MessageInfo CreateModel(MessageInfoBindingModel model, MessageInfo message)
        {
            message.MessageId = model.MessageId;
            message.SenderName = source.Clients.FirstOrDefault(rec => rec.Id == model.ClientId)?.ClientFIO;
            message.DateDelivery = model.DateDelivery;
            message.Subject = model.Subject;
            message.Body = model.Body;
            return message;
        }

        private MessageInfoViewModel CreateModel(MessageInfo message)
        {
            return new MessageInfoViewModel
            {
                MessageId = message.MessageId,
                SenderName = message.SenderName,
                DateDelivery = message.DateDelivery,
                Subject = message.Subject,
                Body = message.Body,
            };
        }

        public MessageInfoViewModel GetElement(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var message = source.MessageInfoes
            .FirstOrDefault(rec => rec.MessageId.Equals(model.MessageId));
            return message != null ? CreateModel(message) : null;
        }

        public void Update(MessageInfoBindingModel model)
        {
            var element = source.MessageInfoes.FirstOrDefault(rec => rec.MessageId.Equals(model.MessageId));
            if (element == null)
            {
                throw new Exception("Сообщение не найдено");
            }
            CreateModel(model, element);
        }
    }
}

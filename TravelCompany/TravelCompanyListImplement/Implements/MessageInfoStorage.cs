using System;
using System.Collections.Generic;
using System.Text;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;
using TravelCompanyListImplement.Models;

namespace TravelCompanyListImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly DataListSingleton source;

        public MessageInfoStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<MessageInfoViewModel> GetFullList()
        {
            List<MessageInfoViewModel> result = new List<MessageInfoViewModel>();
            foreach (var message in source.MessageInfoes)
            {
                result.Add(CreateModel(message));
            }
            return result;
        }
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            int toSkip = model.ToSkip ?? 0;
            int toTake = model.ToTake ?? source.MessageInfoes.Count;
            var result = new List<MessageInfoViewModel>();
            foreach (var message in source.MessageInfoes)
            {
                if (model.ClientId.HasValue ?
                    (message.ClientId == model.ClientId)
                    :
                    (model.ToSkip.HasValue && model.ToTake.HasValue || message.DateDelivery.Date == model.DateDelivery.Date))
                {
                    if (toSkip > 0) { toSkip--; continue; }
                    if (toTake > 0)
                    {
                        result.Add(CreateModel(message));
                        toTake--;
                    }
                }
            }
            return result;
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
            string clientName = string.Empty;
            foreach (var client in source.Clients)
            {

                if (client.Id == model.ClientId)
                {
                    clientName = client.ClientFIO;
                    break;
                }
            }
            message.MessageId = model.MessageId;
            message.SenderName = clientName;
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
            foreach (var message in source.MessageInfoes)
            {
                if (message.MessageId.Equals(model.MessageId))
                {
                    return CreateModel(message);
                }
            }
            return null;
        }

        public void Update(MessageInfoBindingModel model)
        {
            MessageInfo testMessage = null;
            foreach (var message in source.MessageInfoes)
            {
                if (message.MessageId.Equals(model.MessageId))
                {
                    testMessage = message;
                }
            }
            if (testMessage == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, testMessage);
        }
    }
}

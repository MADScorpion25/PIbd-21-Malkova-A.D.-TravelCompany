using System;
using System.Collections.Generic;
using System.Linq;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;
using TravelCompanyDatabaseImplement.Models;

namespace TravelCompanyDatabaseImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        public List<MessageInfoViewModel> GetFullList()
        {
            using var context = new TravelCompanyDatabase();
            return context.MessageInfoes
            .Select(rec => new MessageInfoViewModel
            {
                MessageId = rec.MessageId,
                SenderName = rec.SenderName,
                DateDelivery = rec.DateDelivery,
                Subject = rec.Subject,
                Body = rec.Body
            })
            .ToList();
        }
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelCompanyDatabase();
            return context.MessageInfoes.Where(rec => model.ClientId.HasValue ?
                (rec.ClientId == model.ClientId)
                :
                (model.ToSkip.HasValue && model.ToTake.HasValue || rec.DateDelivery.Date == model.DateDelivery.Date))
                .Skip(model.ToSkip ?? 0)
                .Take(model.ToTake ?? context.MessageInfoes.Count())
                .Select(CreateModel)
                .ToList();
        }
        public void Insert(MessageInfoBindingModel model)
        {
            using var context = new TravelCompanyDatabase();
            MessageInfo element = context.MessageInfoes.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (element != null)
            {
                throw new Exception("Уже есть письмо с таким  идентификатором");
            }
            context.MessageInfoes.Add(new MessageInfo
            {
                MessageId = model.MessageId,
                ClientId = model.ClientId != null ? model.ClientId : context.Clients.FirstOrDefault(rec => rec.Login == model.FromMailAddress).Id,
                SenderName = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            });
            context.SaveChanges();
        }

        public MessageInfoViewModel GetElement(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelCompanyDatabase();
            var message = context.MessageInfoes
            .FirstOrDefault(rec => rec.MessageId.Equals(model.MessageId));
            return message != null ?
            new MessageInfoViewModel
            {
                MessageId = message.MessageId,
                SenderName = message.SenderName,
                DateDelivery = message.DateDelivery,
                Subject = message.Subject,
                Body = message.Body,
                MessageStatus = message.MessageStatus,
                ReplyText = message.ReplyText
            } :
            null;
        }

        public void Update(MessageInfoBindingModel model)
        {
            using var context = new TravelCompanyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.MessageInfoes.FirstOrDefault(rec => rec.MessageId.Equals(model.MessageId));
                if (element == null)
                {
                    throw new Exception("Сообщение не найдено");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        private MessageInfoViewModel CreateModel(MessageInfo model)
        {
            return new MessageInfoViewModel
            {
                MessageId = model.MessageId,
                SenderName = model.SenderName,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body,
                MessageStatus = model.MessageStatus,
                ReplyText = model.ReplyText
            };
        }
        private MessageInfo CreateModel(MessageInfoBindingModel model, MessageInfo message)
        {
            message.MessageId = model.MessageId;
            message.ClientId = model.ClientId;
            message.Subject = model.Subject;
            message.Body = model.Body;
            message.DateDelivery = model.DateDelivery;
            message.ReplyText = model.ReplyText;
            message.MessageStatus = model.MessageStatus;
            return message;
        }
    }
}


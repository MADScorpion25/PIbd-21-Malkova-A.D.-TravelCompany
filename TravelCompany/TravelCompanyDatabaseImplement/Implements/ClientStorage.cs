using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;
using TravelCompanyDatabaseImplement.Models;

namespace TravelCompanyDatabaseImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        public void Delete(ClientBindingModel model)
        {
            using (var context = new TravelCompanyDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Клиент не найден");
                }
            }
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelCompanyDatabase())
            {
                var client = context.Clients.Include(x => x.Orders)
                .FirstOrDefault(rec => rec.Login == model.Login || rec.Id == model.Id);
                return client != null ? CreateModel(client) : null;
            }
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelCompanyDatabase())
            {
                return context.Clients.Include(x => x.Orders)
                .Where(rec => rec.Login == model.Login && rec.Password == model.Password)
                .Select(CreateModel)
                .ToList();
            }
        }

        public List<ClientViewModel> GetFullList()
        {
            using (var context = new TravelCompanyDatabase())
            {
                return context.Clients.Select(CreateModel).ToList();
            }
        }

        public void Insert(ClientBindingModel model)
        {
            using (var context = new TravelCompanyDatabase())
            {
                context.Clients.Add(CreateModel(model, new Client()));
                context.SaveChanges();
            }
        }

        public void Update(ClientBindingModel model)
        {
            using (var context = new TravelCompanyDatabase())
            {
                var element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Клиент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        private Client CreateModel(ClientBindingModel model, Client client)
        {
            client.ClientFIO = model.ClientFIO;
            client.Login = model.Login;
            client.Password = model.Password;
            return client;
        }
        private ClientViewModel CreateModel(Client model)
        {
            return new ClientViewModel
            {
                Id = model.Id,
                ClientFIO = model.ClientFIO,
                Login = model.Login,
                Password = model.Password
            };
        }
    }
}
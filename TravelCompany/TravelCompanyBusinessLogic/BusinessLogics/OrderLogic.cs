using System;
using System.Collections.Generic;
using TravelCompanyBusinessLogic.MailWorker;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.BusinessLogicsContracts;
using TravelCompanyContracts.Enums;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        private readonly IWarehouseStorage _warehouseStorage;
        private readonly ITravelStorage _travelStorage;
        private readonly IClientStorage _clientStorage;
        private readonly AbstractMailWorker _mailWorker;
        public OrderLogic(IOrderStorage orderStorage, IWarehouseStorage warehouseStorage, ITravelStorage travelStorage, IClientStorage clientStorage, AbstractMailWorker mailWorker)
        {
            _orderStorage = orderStorage;
            _warehouseStorage = warehouseStorage;
            _travelStorage = travelStorage;
            _clientStorage = clientStorage;
            _mailWorker = mailWorker;
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                TravelId = model.TravelId,
                ClientId = model.ClientId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel
                {
                    Id = model.ClientId
                })?.Login,
                Subject = $"Новый заказ",
                Text = $"Заказ от {DateTime.Now} на сумму {model.Sum:N2} принят."
            });
        }

        public void DeliveryOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                TravelId = order.TravelId,
                ClientId = order.ClientId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Выдан,
                ImplementerId = order.ImplementerId
            });
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel
                {
                    Id = order.ClientId
                })?.Login,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} выдан."
            });
        }

        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                TravelId = order.TravelId,
                ClientId = order.ClientId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Готов,
                ImplementerId = model.ImplementerId
            });
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = _clientStorage.GetElement(new ClientBindingModel
                {
                    Id = order.ClientId
                })?.Login,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} выполнен."
            });
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            OrderViewModel order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Принят && order.Status != OrderStatus.Требуются_материалы)
            {
                throw new Exception("Заказ еще не принят");
            }

            var updateBindingModel = new OrderBindingModel
            {
                Id = order.Id,
                TravelId = order.TravelId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                ClientId = order.ClientId
            };

            if (!_warehouseStorage.TakeConditionFromWarehouse(_travelStorage.GetElement
                (new TravelBindingModel { Id = order.TravelId }).TravelConditions, order.Count))
            {
                updateBindingModel.Status = OrderStatus.Требуются_материалы;
            }
            else
            {
                updateBindingModel.DateImplement = DateTime.Now;
                updateBindingModel.Status = OrderStatus.Выполняется;
                updateBindingModel.ImplementerId = model.ImplementerId;
            }

            _orderStorage.Update(updateBindingModel);
        }
    }
}


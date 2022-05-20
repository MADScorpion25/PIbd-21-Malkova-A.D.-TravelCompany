using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.BusinessLogicsContracts;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly ITravelLogic _travel;
        private readonly IMessageInfoLogic _message;
        private readonly int page_size = 4;
        public MainController(IOrderLogic order, ITravelLogic travel, IMessageInfoLogic message)
        {
            _order = order;
            _travel = travel;
            _message = message;
        }
        [HttpGet]
        public List<TravelViewModel> GetTravelList() => _travel.Read(null)?.ToList();
        [HttpGet]
        public TravelViewModel GetTravel(int travelId) => _travel.Read(new TravelBindingModel
        { Id = travelId })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _order.CreateOrder(model);
        [HttpGet]
        public (List<MessageInfoViewModel>, bool) GetMessages(int clientId, int page)
        {
             var list = _message.Read(new MessageInfoBindingModel
            {
                ClientId = clientId,
                ToSkip = (page - 1) * page_size,
                ToTake = page_size + 1
            })
                .ToList();
            var hasNext = !(list.Count() <= page_size);
            return (list.Take(page_size).ToList(), hasNext);
        }
    }
}


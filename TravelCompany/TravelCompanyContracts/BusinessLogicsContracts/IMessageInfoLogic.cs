using System.Collections.Generic;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyContracts.BusinessLogicsContracts
{
    public interface IMessageInfoLogic
    {
        List<MessageInfoViewModel> Read(MessageInfoBindingModel model);
        void CreateOrUpdate(MessageInfoBindingModel model);
    }
}

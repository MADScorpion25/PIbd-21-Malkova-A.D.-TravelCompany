using System;
using System.Collections.Generic;
using System.Text;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyContracts.StorageContracts
{
    public interface IMessageInfoStorage
    {
        List<MessageInfoViewModel> GetFullList();
        List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model);
        void Insert(MessageInfoBindingModel model);
    }
}

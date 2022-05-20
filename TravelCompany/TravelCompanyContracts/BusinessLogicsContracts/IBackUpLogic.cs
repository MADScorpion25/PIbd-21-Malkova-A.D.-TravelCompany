using System;
using System.Collections.Generic;
using System.Text;
using TravelCompanyContracts.BindingModels;

namespace TravelCompanyContracts.BusinessLogicsContracts
{
    public interface IBackUpLogic
    {
        void CreateBackUp(BackUpSaveBinidngModel model);
    }
}

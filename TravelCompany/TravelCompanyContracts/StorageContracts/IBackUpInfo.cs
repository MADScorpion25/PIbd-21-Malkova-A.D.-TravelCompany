using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TravelCompanyContracts.StorageContracts
{
    public interface IBackUpInfo
    {
        Assembly GetAssembly();
        List<PropertyInfo> GetFullList();
        List<T> GetList<T>() where T : class, new();
    }
}

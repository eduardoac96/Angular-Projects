using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreU_WepApi.Constants
{
    public enum RegisterWebStoreStatus
    { 
        AccountInformation = 10,
        PersonalInformation = 20,
        BusinessInformation = 30,
        RegisterCard = 40
    }

    public enum RegisterLocalStoreStatus
    {
        AccountInformation = 10,
        PersonalInformation = 20,
        BusinessInformation = 30,
        RegisterCard = 40
    }

    public enum RegisterCustomer
    {
        AccountInformation = 10,
        PersonalInformation = 20,  
        RegisterCard = 40
    }
}

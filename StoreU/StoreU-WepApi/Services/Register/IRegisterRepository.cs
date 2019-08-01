using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreU_WepApi.Services.Register
{
    public interface IRegisterRepository
    {
        Task<bool> SaveAsync(); 
    }
}

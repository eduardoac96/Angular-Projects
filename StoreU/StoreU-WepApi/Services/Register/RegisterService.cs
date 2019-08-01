using StoreU_WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreU_WepApi.Services.Register
{
    public class RegisterService : IRegisterRepository
    {
        private StoreUContext _context;

        public RegisterService(StoreUContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync()) >= 0;
        }


    }
}

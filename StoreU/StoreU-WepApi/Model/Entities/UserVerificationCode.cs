using StoreU_WebApi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreU_WepApi.Model.Entities
{
    public class UserVerificationCode
    { 
        [Key]
        public Guid VerificationId { get; set; }

        public Guid UserId { get; set; }
        public int SecurityCode { get; set; }
        public DateTime RegistryDate { get; set; }
        public DateTime ExpirationTime { get; set; }

        public bool IsUsed { get; set; }
        public virtual Users Users { get; set; }
    }
}

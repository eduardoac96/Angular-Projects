using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreU_WebApi.Model
{
    public partial class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }
         
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        //public int IdRole { get; set; }
        public int RoleId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}

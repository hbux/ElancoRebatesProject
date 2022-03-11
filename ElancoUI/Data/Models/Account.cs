using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoUI.Data.Models
{
    public class Account
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }
        public IdentityUser User { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<Pet> Pets { get; set; } = new List<Pet>();
    }
}

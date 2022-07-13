using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ElancoUI.Models.DbContextModels
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

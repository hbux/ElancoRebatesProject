﻿using ElancoUI.Models.DbContextModels;
using System.ComponentModel.DataAnnotations;

namespace ElancoUI.Models
{
    public class AccountModel
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(50)]
        public string State { get; set; }

        [MaxLength(10)]
        public string ZipCode { get; set; }

        public List<Pet> Pets { get; set; } = new List<Pet>();
    }
}
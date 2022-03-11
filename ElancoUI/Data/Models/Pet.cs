using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoUI.Data.Models
{
    public class Pet
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public string ImageName { get; set; }

        [MaxLength(10)]
        public string ImageType { get; set; }
    }
}

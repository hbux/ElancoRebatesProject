using System.ComponentModel.DataAnnotations;

namespace ElancoUI.Models.DbContextModels
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

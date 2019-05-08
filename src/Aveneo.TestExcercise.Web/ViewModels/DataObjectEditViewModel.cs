using System.ComponentModel.DataAnnotations;

namespace Aveneo.TestExcercise.Web.ViewModels
{
    public class DataObjectEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        [Required]
        public decimal Latitude { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aveneo.TestExcercise.ApplicationCore.Entities
{
    public class Geography
    {
        [Required]
        [Column(TypeName = "decimal(19,4)")]
        public decimal Latitude { get; set; }
        [Required]
        [Column(TypeName = "decimal(19,4)")]
        public decimal Longitude { get; set; }
    }
}
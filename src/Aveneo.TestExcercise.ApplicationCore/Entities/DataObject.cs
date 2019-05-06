using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aveneo.TestExcercise.ApplicationCore.Entities
{
    public class DataObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(19,4)")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Required]
        public Geography Location { get; set; }
        public ICollection<DataObjectFeature> Features { get; set; }
    }
}

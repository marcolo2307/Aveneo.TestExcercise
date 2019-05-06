using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aveneo.TestExcercise.ApplicationCore.Entities
{
    public class DataObjectFeature
        : AutoIncEntityBase
    {
        [Required]
        public DataObject DataObject { get; set; }
        [Required]
        public Feature Feature { get; set; }
    }
}

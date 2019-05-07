using System.ComponentModel.DataAnnotations;

namespace Aveneo.TestExcercise.ApplicationCore.Entities
{
    public class DataObjectFeature
        : EntityBase
    {
        [Required]
        public DataObject DataObject { get; set; }
        [Required]
        public Feature Feature { get; set; }
    }
}

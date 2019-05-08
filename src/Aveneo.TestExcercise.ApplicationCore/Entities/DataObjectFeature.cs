using System.ComponentModel.DataAnnotations;

namespace Aveneo.TestExcercise.ApplicationCore.Entities
{
    public class DataObjectFeature
        : EntityBase
    {
        [Required]
        public int DataObjectId { get; set; }
        [Required]
        public int FeatureId { get; set; }
    }
}

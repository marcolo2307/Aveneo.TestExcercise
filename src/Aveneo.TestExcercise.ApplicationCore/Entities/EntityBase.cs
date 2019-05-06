using System.ComponentModel.DataAnnotations;

namespace Aveneo.TestExcercise.ApplicationCore.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}

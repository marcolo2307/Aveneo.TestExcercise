using System.ComponentModel.DataAnnotations;

namespace Aveneo.TestExcercise.Web.ViewModels
{
    public class FeatureEditViewModel
    {
        public int Id { get; set; }
        [Required]
        public string IconName { get; set; }
    }
}

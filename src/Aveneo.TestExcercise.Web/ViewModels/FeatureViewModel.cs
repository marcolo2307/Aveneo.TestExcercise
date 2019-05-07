using System.ComponentModel.DataAnnotations;

namespace Aveneo.TestExcercise.Web.ViewModels
{
    public class FeatureViewModel
    {
        public int Id { get; set; }
        [Required]
        public string IconName { get; set; }
        public string IconHtml { get; set; }
    }
}

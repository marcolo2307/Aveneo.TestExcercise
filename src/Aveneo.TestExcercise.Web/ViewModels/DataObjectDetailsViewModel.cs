using System.Collections.Generic;

namespace Aveneo.TestExcercise.Web.ViewModels
{
    public class DataObjectDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string DefaultPhoto { get; set; }
        public ICollection<FeatureDetailsViewModel> Features { get; set; }
    }
}

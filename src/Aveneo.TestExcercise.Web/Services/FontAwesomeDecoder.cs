using Aveneo.TestExcercise.ApplicationCore.Entities;

namespace Aveneo.TestExcercise.Web.Services
{
    public class FontAwesomeDecoder : IIconDecoder
    {
        public string Decode(Feature feature)
        {
            return $"<i class=\"{feature.IconName}\"></i>";
        }
    }
}

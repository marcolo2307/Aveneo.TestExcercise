namespace Aveneo.TestExcercise.Web.Services.Implementations
{
    public class FontAwesomeDecoder : IIconDecoder
    {
        public string Decode(string encoded)
        {
            return $"<i class=\"{encoded}\"></i>";
        }
    }
}

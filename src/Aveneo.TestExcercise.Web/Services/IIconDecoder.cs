using Aveneo.TestExcercise.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aveneo.TestExcercise.Web.Services
{
    public interface IIconDecoder
    {
        string Decode(string feature);
    }
}

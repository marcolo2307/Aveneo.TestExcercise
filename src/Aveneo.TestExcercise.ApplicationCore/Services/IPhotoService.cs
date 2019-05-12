using System.IO;
using System.Threading.Tasks;

namespace Aveneo.TestExcercise.ApplicationCore.Services
{
    public interface IPhotoService
    {
        Task<Stream> GetAsync(string filename);
        Task CreateAsync(string filename, Stream data);
        Task DeleteAsync(string filename);
    }
}

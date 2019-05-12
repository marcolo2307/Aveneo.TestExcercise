using System.IO;
using System.Threading.Tasks;
using Aveneo.TestExcercise.ApplicationCore.Services;

namespace Aveneo.TestExcercise.Infrastructure.Services.Implementations
{
    public class HardDrivePhotoService : IPhotoService
    {
        private string Path { get; }

        public HardDrivePhotoService()
        {
            Path = "C:\\TempPhotos\\";
            Directory.CreateDirectory("C:\\TempPhotos\\");
        }

        public async Task CreateAsync(string filename, Stream data)
        {
            data.Position = 0;
            using (FileStream file = File.Create(Path + filename))
            {
                await data.CopyToAsync(file);
            }
        }

        public Task DeleteAsync(string filename)
        {
            return Task.Run(() => File.Delete(Path + filename));
        }

        public async Task<Stream> GetAsync(string filename)
        {
            using (FileStream fileStream = File.Open(Path + filename, FileMode.Open))
            {
                var memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                return memoryStream;
            }
        }
    }
}

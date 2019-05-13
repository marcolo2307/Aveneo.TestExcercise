using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aveneo.TestExcercise.ApplicationCore;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.ApplicationCore.Services;

namespace Aveneo.TestExcercise.Infrastructure.Services.Implementations
{
    public class HardDrivePhotoService : IPhotoService
    {
        private string Path { get; }

        public HardDrivePhotoService(IRepository<Configuration> configurations)
        {
            var findConfigurationTask = configurations.WhereAsync(e => e.Key == ConfigurationKeys.FileLocation);
            findConfigurationTask.Wait();

            var config = findConfigurationTask.Result.FirstOrDefault();
            if (config == null || string.IsNullOrEmpty(config.Value))
                Path = "C:\\TempPhotos\\";
            else
                Path = config.Value;
            Directory.CreateDirectory(Path);

            if (Path.Last() != '\\')
                Path += '\\';
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

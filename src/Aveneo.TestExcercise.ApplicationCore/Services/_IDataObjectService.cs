using Aveneo.TestExcercise.ApplicationCore.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Aveneo.TestExcercise.ApplicationCore.Services
{
    public interface _IDataObjectService
    {
        IRepository<DataObject> DataObjects { get; }

        Task<ICollection<Feature>> GetFeaturesAsync(DataObject dataObject);
        Task SetFeaturesAsync(DataObject dataObject, IEnumerable<Feature> features);

        Task<ICollection<Photo>> GetPhotosAsync(DataObject dataObject);
        Task<Photo> GetDefaultPhotoAsync(DataObject dataObject);

        Task AddNewPhotoAsync(DataObject dataObject, Stream photo);
        Task UpdateExistingPhotos(DataObject dataObject, IEnumerable<Photo> photos);

        Task DeleteAsync(DataObject dataObject);
    }
}

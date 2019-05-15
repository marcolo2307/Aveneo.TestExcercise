using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aveneo.TestExcercise.ApplicationCore.Entities;

namespace Aveneo.TestExcercise.ApplicationCore.Services.Implementations
{
    public class _DataObjectService : _IDataObjectService
    {
        public IRepository<DataObject> DataObjects { get; }
        private IRepository<Feature> _features { get; }
        private IRepository<DataObjectFeature> _dataObjectFeatures { get; }
        private IRepository<DataObjectGallery> _dataObjectGalleries { get; }
        private IPhotoService _photoService { get; }

        public _DataObjectService(
            IRepository<DataObject> dataObjects,
            IRepository<Feature> features,
            IRepository<DataObjectFeature> dataObjectFeatures,
            IRepository<DataObjectGallery> dataObjectGalleries,
            IPhotoService photoService)
        {
            DataObjects = dataObjects;
            _features = features;
            _dataObjectFeatures = dataObjectFeatures;
            _dataObjectGalleries = dataObjectGalleries;
            _photoService = photoService;
        }

        public async Task<ICollection<Feature>> GetFeaturesAsync(DataObject dataObject)
        {
            var dataObjectFeatures = await _dataObjectFeatures
                .WhereAsync(dof => dof.DataObjectId == dataObject.Id);

            var featuresIds = dataObjectFeatures.Select(dof => dof.FeatureId);

            var features = await _features.WhereAsync(f => featuresIds.Contains(f.Id));

            return features;
        }

        public async Task SetFeaturesAsync(DataObject dataObject, IEnumerable<Feature> features)
        {
            var exitstingBindings = await _dataObjectFeatures
                .WhereAsync(dof => dof.DataObjectId == dataObject.Id);
            await _dataObjectFeatures.DeleteAsync(exitstingBindings);

            var bindings = features.ToList().ConvertAll<DataObjectFeature>(f => new DataObjectFeature
            {
                DataObjectId = dataObject.Id,
                FeatureId = f.Id
            });

            await _dataObjectFeatures.CreateAsync(bindings);
        }

        public async Task<Photo> GetDefaultPhotoAsync(DataObject dataObject)
        {
            var photos = await GetPhotosAsync(dataObject);
            return photos.FirstOrDefault();
        }

        public async Task<ICollection<Photo>> GetPhotosAsync(DataObject dataObject)
        {
            var bindedGalleries = (await _dataObjectGalleries
                .WhereAsync(e => e.DataObjectId == dataObject.Id))
                .OrderBy(e => e.Sequence).ToList();

            var photos = await Task.WhenAll(bindedGalleries.Select(async g => new Photo
            {
                Id = g.Id,
                Sequence = g.Sequence,
                Source = await GetPhotoData(g)
            }));

            return photos;
        }

        private async Task<string> GetPhotoData(DataObjectGallery gallery)
        {
            var stream = await _photoService.GetAsync(gallery.FileName.ToString());
            using (var reader = new StreamReader(stream))
                return await reader.ReadToEndAsync();
        }

        public async Task AddNewPhotoAsync(DataObject dataObject, Stream photo)
        {
            var gallery = new DataObjectGallery
            {
                DataObjectId = dataObject.Id,
                FileName = Guid.NewGuid()
            };

            var existingGalleries = await _dataObjectGalleries.WhereAsync(g => g.DataObjectId == dataObject.Id);
            existingGalleries = existingGalleries.OrderBy(e => e.Sequence).ToList();
            gallery.Sequence = existingGalleries.LastOrDefault()?.Sequence + 1 ?? 0;

            await _dataObjectGalleries.CreateAsync(gallery);

            var stream = new MemoryStream();
            await photo.CopyToAsync(stream);

            var base64 = Convert.ToBase64String(stream.ToArray());
            stream.SetLength(0);
            var writer = new StreamWriter(stream);
            writer.Write(base64);

            await _photoService.CreateAsync(gallery.FileName.ToString(), stream);
        }

        public async Task UpdateExistingPhotos(DataObject dataObject, IEnumerable<Photo> photos)
        {
            var galleries = await _dataObjectGalleries.WhereAsync(g => g.DataObjectId == dataObject.Id);

            var updatedGalleries = new List<DataObjectGallery>();
            foreach (var photo in photos)
            {
                var gallery = galleries.FirstOrDefault(g => g.Id == photo.Id);
                if (gallery == null)
                    continue;
                gallery.Sequence = photo.Sequence;
                updatedGalleries.Add(gallery);
            }

            var toDelete = galleries.Except(updatedGalleries);
            foreach (var gallery in toDelete)
                await DeleteGalleryAsync(gallery);

            await _dataObjectGalleries.UpdateAsync(updatedGalleries);
        }

        private async Task DeleteGalleryAsync(DataObjectGallery gallery)
        {
            await _photoService.DeleteAsync(gallery.FileName.ToString());

            await _dataObjectGalleries.DeleteAsync(gallery);
        }

        public async Task DeleteAsync(DataObject dataObject)
        {
            var features = await _dataObjectFeatures.WhereAsync(f => f.DataObjectId == dataObject.Id);
            await _dataObjectFeatures.DeleteAsync(features);

            var galleries = await _dataObjectGalleries.WhereAsync(g => g.DataObjectId == dataObject.Id);
            foreach (var gallery in galleries)
                await DeleteGalleryAsync(gallery);

            await DataObjects.DeleteAsync(dataObject);
        }
    }
}

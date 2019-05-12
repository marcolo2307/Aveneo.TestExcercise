using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aveneo.TestExcercise.ApplicationCore.Entities;

namespace Aveneo.TestExcercise.ApplicationCore.Services.Implementations
{
    public class DataObjectGalleryService : IDataObjectGalleryService
    {
        private IDataObjectService _dataObjectService { get; }
        private IRepository<DataObjectGallery> _galleries { get; }

        public DataObjectGalleryService(IDataObjectService dataObjectService, IRepository<DataObjectGallery> galleries)
        {
            _dataObjectService = dataObjectService;
            _galleries = galleries;
        }

        public async Task<ICollection<DataObjectGallery>> GetAllAsync(DataObject dataObject)
        {
            var bindedGalleries = await _galleries.WhereAsync(e => e.DataObjectId == dataObject.Id);
            return bindedGalleries.OrderBy(e => e.Sequence).ToList();
        }

        public async Task<DataObjectGallery> AddAsync(DataObject dataObject, Guid filename)
        {
            var gallery = new DataObjectGallery
            {
                DataObjectId = dataObject.Id,
                FileName = filename
            };

            var existingGalleries = await GetAllAsync(dataObject);
            existingGalleries = existingGalleries.OrderBy(e => e.Sequence).ToList();
            gallery.Sequence = existingGalleries.LastOrDefault()?.Sequence + 1 ?? 0;

            await _galleries.CreateAsync(gallery);
            return gallery;
        }

        public async Task RemoveAsync(DataObjectGallery gallery)
        {
            await _galleries.DeleteAsync(gallery);
        }

        public async Task MoveDownAsync(DataObjectGallery gallery)
        {
            var galleries = await _galleries.WhereAsync(e => e.DataObjectId == gallery.DataObjectId);
            if (galleries.Count <= 1 || galleries.Last().Id == gallery.Id)
                return;
            
            for (var i = 0; i<galleries.Count - 1; i++)
            {
                if (galleries.ElementAt(i).Id == gallery.Id)
                {
                    galleries.ElementAt(i).Sequence++;
                    galleries.ElementAt(i + 1).Sequence--;
                    await _galleries.UpdateAsync(galleries.ElementAt(i));
                    await _galleries.UpdateAsync(galleries.ElementAt(i + 1));
                    break;
                }
            }
        }

        public async Task MoveUpAsync(DataObjectGallery gallery)
        {
            var galleries = await _galleries.WhereAsync(e => e.DataObjectId == gallery.DataObjectId);
            if (galleries.Count <= 1 || galleries.First().Id == gallery.Id)
                return;

            for (var i = 1; i < galleries.Count; i++)
            {
                if (galleries.ElementAt(i).Id == gallery.Id)
                {
                    galleries.ElementAt(i).Sequence--;
                    galleries.ElementAt(i - 1).Sequence++;
                    await _galleries.UpdateAsync(galleries.ElementAt(i));
                    await _galleries.UpdateAsync(galleries.ElementAt(i - 1));
                    break;
                }
            }
        }
    }
}

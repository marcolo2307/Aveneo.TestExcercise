using Aveneo.TestExcercise.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aveneo.TestExcercise.ApplicationCore.Services
{
    public interface IDataObjectGalleryService
    {
        Task<ICollection<DataObjectGallery>> GetAllAsync(DataObject dataObject);
        Task<DataObjectGallery> AddAsync(DataObject dataObject, Guid filename);
        Task RemoveAsync(DataObjectGallery gallery);
        Task MoveUpAsync(DataObjectGallery gallery);
        Task MoveDownAsync(DataObjectGallery gallery);
    }
}

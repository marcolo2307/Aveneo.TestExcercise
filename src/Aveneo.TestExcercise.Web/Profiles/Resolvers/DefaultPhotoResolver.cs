using AutoMapper;
using Aveneo.TestExcercise.ApplicationCore;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.ApplicationCore.Services;
using Aveneo.TestExcercise.Web.ViewModels;
using System.IO;
using System.Linq;

namespace Aveneo.TestExcercise.Web.Profiles.Resolvers
{
    public class DefaultPhotoResolver : IValueResolver<DataObject, DataObjectDetailsViewModel, string>
    {
        private IRepository<DataObjectGallery> _galleries { get; }
        private IPhotoService _photoService { get; }

        public DefaultPhotoResolver(
            IRepository<DataObjectGallery> galleries,
            IPhotoService photoService)
        {
            _galleries = galleries;
            _photoService = photoService;
        }


        public string Resolve(DataObject source, DataObjectDetailsViewModel destination, string destMember, ResolutionContext context)
        {
            var galleryTask = _galleries.WhereAsync(e => e.DataObjectId == source.Id && e.Sequence == 0);
            galleryTask.Wait();

            var gallery = galleryTask.Result.FirstOrDefault();
            if (gallery == null)
                return "";

            var photoTask = _photoService.GetAsync(gallery.FileName.ToString());
            photoTask.Wait();

            using (var reader = new StreamReader(photoTask.Result))
                return reader.ReadToEnd();
        }
    }
}

using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.ApplicationCore.Services;
using Aveneo.TestExcercise.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.TestExcercise.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {

        private _IDataObjectService _dataObjectService { get; }

        public PhotosController(
            _IDataObjectService dataObjectService)
        {
            _dataObjectService = dataObjectService;
        }

        [HttpGet("{objectId}")]
        public async Task<ActionResult<ICollection<PhotoViewModel>>> GetAllPhotos(int objectId)
        {
            var dataObject = await _dataObjectService.DataObjects.FindByIdAsync(objectId);

            if (dataObject == null)
                return NotFound();

            var photos = await _dataObjectService.GetPhotosAsync(dataObject);

            var photosViewModels = photos.ToList().ConvertAll<PhotoViewModel>(p => new PhotoViewModel
            {
                Id = p.Id,
                Sequence = p.Sequence,
                Photo = p.Source
            });

            return Ok(photosViewModels);
        }

        [HttpPut("{objectId}")]
        public async Task<IActionResult> UpdatePhotos(int objectId, [FromBody] UpdatePhotosViewModel viewModel)
        {
            var dataObject = await _dataObjectService.DataObjects.FindByIdAsync(objectId);

            if (dataObject == null)
                return NotFound();

            if (viewModel.Photos.Count != viewModel.Photos.Select(p => p.Sequence).Distinct().Count())
                return BadRequest();

            var photos = viewModel.Photos.ToList().ConvertAll<Photo>(p => new Photo
            {
                Id = p.Id,
                Sequence = p.Sequence,
                Source = p.Photo
            });

            await _dataObjectService.UpdateExistingPhotos(dataObject, photos);

            return NoContent();
        }

        [HttpPost("{objectId}")]
        public async Task<IActionResult> UploadPhotos(int objectId, IFormFileCollection photos)
        {
            var dataObject = await _dataObjectService.DataObjects.FindByIdAsync(objectId);

            if (dataObject == null)
                return NotFound();

            foreach (var photo in photos)
            {
                var stream = photo.OpenReadStream();
                await _dataObjectService.AddNewPhotoAsync(dataObject, stream);
            }

            return NoContent();
        }
    }
}
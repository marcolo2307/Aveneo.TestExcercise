﻿using Aveneo.TestExcercise.ApplicationCore;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.ApplicationCore.Services;
using Aveneo.TestExcercise.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private IRepository<DataObject> _dataObjects { get; }
        private IDataObjectGalleryService _dataObjectGalleryService { get; }
        private IPhotoService _photoService { get; }

        public PhotosController(
            IRepository<DataObject> dataObjects,
            IDataObjectGalleryService dataObjectGalleryService,
            IPhotoService photoService)
        {
            _dataObjects = dataObjects;
            _dataObjectGalleryService = dataObjectGalleryService;
            _photoService = photoService;
        }

        [HttpGet("{objectId}")]
        public async Task<ActionResult<ICollection<PhotoViewModel>>> GetAllPhotos(int objectId)
        {
            var dataObject = await _dataObjects.FindByIdAsync(objectId);

            if (dataObject == null)
                return NotFound();

            var galleries = (await _dataObjectGalleryService.GetAllAsync(dataObject))
                .OrderBy(e => e.Sequence).ToList();

            var photos = new List<PhotoViewModel>();
            foreach (var g in galleries)
            {
                var stream = await _photoService.GetAsync(g.FileName.ToString());
                var reader = new StreamReader(stream);
                    
                photos.Add(new PhotoViewModel
                {
                    Id = g.Id,
                    Sequence = g.Sequence,
                    Photo = await reader.ReadToEndAsync()
                });

                reader.Dispose();
            }

            return Ok(photos);
        }

        [HttpPut("{objectId}")]
        public async Task<IActionResult> UpdatePhotos(int objectId)
        {
            var dataObject = await _dataObjects.FindByIdAsync(objectId);

            if (dataObject == null)
                return NotFound();



            return NoContent();
        }

        [HttpPost("{objectId}")]
        public async Task<IActionResult> UploadPhotos(int objectId, IFormFileCollection photos)
        {
            var dataObject = await _dataObjects.FindByIdAsync(objectId);

            if (dataObject == null)
                return NotFound();

            foreach (var photo in photos)
            {
                var stream = new MemoryStream();
                await photo.CopyToAsync(stream);

                var base64 = Convert.ToBase64String(stream.ToArray());
                stream.SetLength(0);
                var writer = new StreamWriter(stream);
                writer.Write(base64);

                var filename = Guid.NewGuid();
                await _photoService.CreateAsync(filename.ToString(), stream);

                await _dataObjectGalleryService.AddAsync(dataObject, filename);
            }

            return NoContent();
        }
    }
}
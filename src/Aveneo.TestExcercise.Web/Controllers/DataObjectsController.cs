﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.ApplicationCore;
using AutoMapper;
using Aveneo.TestExcercise.Web.ViewModels;
using Aveneo.TestExcercise.Web.Services;

namespace Aveneo.TestExcercise.Web.Controllers
{
    public class DataObjectsController : Controller
    {
        private IRepository<DataObject> _dataObjects { get; }
        private IMapper _mapper { get; }
        private IIconDecoder _iconDecoder { get; }

        public DataObjectsController(IRepository<DataObject> dataObjects, IMapper mapper, IIconDecoder iconDecoder)
        {
            _dataObjects = dataObjects;
            _mapper = mapper;
            _iconDecoder = iconDecoder;
        }

        // GET: DataObjects
        public async Task<IActionResult> Index()
        {
            var models = await _dataObjects.GetAllAsync();

            var viewModels = _mapper.Map<ICollection<DataObjectViewModel>>(models);

            return View(viewModels);
        }

        // GET: DataObjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var dataObject = await _dataObjects.FindByIdAsync(id.Value);
            if (dataObject == null)
                return NotFound();

            var viewModel = _mapper.Map<DataObjectViewModel>(dataObject);
            if (viewModel.Features == null)
                viewModel.Features = new List<FeatureViewModel>();

            foreach (var f in viewModel.Features)
                f.IconHtml = _iconDecoder.Decode(new Feature { IconName = f.IconName });

            return View(viewModel);
        }

        // GET: DataObjects/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DataObjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dataObject = _mapper.Map<DataObject>(viewModel);
                await _dataObjects.CreateAsync(dataObject);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: DataObjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var dataObject = await _dataObjects.FindByIdAsync(id.Value);
            if (dataObject == null)
                return NotFound();

            var viewModel = _mapper.Map<DataObjectViewModel>(dataObject);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DataObjectViewModel viewModel)
        {
            var dataObject = await _dataObjects.FindByIdAsync(id);
            if (dataObject == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                _mapper.Map(viewModel, dataObject);

                await _dataObjects.UpdateAsync(dataObject);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: DataObjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataObject = await _dataObjects.FindByIdAsync(id.Value);
            if (dataObject == null)
                return NotFound();

            var viewModel = _mapper.Map<DataObjectViewModel>(dataObject);

            return View(viewModel);
        }

        // POST: DataObjects/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataObject = await _dataObjects.FindByIdAsync(id);
            if (dataObject == null)
                return NotFound();
            await _dataObjects.DeleteAsync(dataObject);
            return RedirectToAction(nameof(Index));
        }
    }
}

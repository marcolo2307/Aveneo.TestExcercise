using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.ApplicationCore;
using AutoMapper;
using Aveneo.TestExcercise.Web.ViewModels;
using Aveneo.TestExcercise.Web.Services;
using System.Linq;

namespace Aveneo.TestExcercise.Web.Controllers
{
    public class DataObjectsController : Controller
    {
        private IMapper _mapper { get; }
        private IIconDecoder _iconDecoder { get; }
        private IDataObjectService _dataObjectService { get; }
        private IRepository<DataObject> _dataObjects { get; }
        private IRepository<Feature> _features { get; }

        public DataObjectsController(
            IMapper mapper,
            IIconDecoder iconDecoder,
            IDataObjectService dataObjectService,
            IRepository<DataObject> dataObjects,
            IRepository<Feature> features)
        {
            _mapper = mapper;
            _iconDecoder = iconDecoder;
            _dataObjectService = dataObjectService;
            _dataObjects = dataObjects;
            _features = features;
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
            var features = dataObject.Features?.Select(e => e.Feature);
            viewModel.Features = _mapper.Map<ICollection<FeatureViewModel>>(features);

            foreach (var f in viewModel.Features)
                f.IconHtml = _iconDecoder.Decode(f.IconName);

            return View(viewModel);
        }

        // GET: DataObjects/Create
        public async Task<IActionResult> Create()
        {
            var features = new List<Feature>(await _features.GetAllAsync());

            var viewModel = new EditDataObjectViewModel
            {
                AvailableFeatures = features,
                Selections = new List<bool>(new bool[features.Count]),
                SelectionsIds = new List<int>(new int[features.Count])
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EditDataObjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var intermediateViewModel = _mapper.Map<DataObjectViewModel>(viewModel);
                var dataObject = _mapper.Map<DataObject>(intermediateViewModel);

                var selected = new List<Feature>();
                for (var i = 0; i < viewModel.Selections.Count; i++)
                {
                    if (viewModel.Selections[i])
                    {
                        var id = viewModel.SelectionsIds[i];
                        var feature = await _features.FindByIdAsync(id);
                        selected.Add(feature);
                    }
                }

                await _dataObjects.CreateAsync(dataObject);
                await _dataObjectService.SetFeaturesAsync(dataObject, selected);

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
            var features = dataObject.Features?.Select(e => e.Feature);
            viewModel.Features = _mapper.Map<ICollection<FeatureViewModel>>(features);

            var editViewModel = _mapper.Map<EditDataObjectViewModel>(viewModel);
            editViewModel.AvailableFeatures = new List<Feature>(await _features.GetAllAsync());
            editViewModel.Selections = new List<bool>(new bool[editViewModel.AvailableFeatures.Count]);

            foreach (var f in dataObject.Features.Select(e => e.Feature))
            {
                var index = editViewModel.AvailableFeatures.IndexOf(f);
                editViewModel.Selections[index] = true;
            }

            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditDataObjectViewModel viewModel)
        {
            var dataObject = await _dataObjects.FindByIdAsync(id);
            if (dataObject == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                var intermediateViewModel = _mapper.Map<DataObjectViewModel>(viewModel);
                _mapper.Map(intermediateViewModel, dataObject);

                await _dataObjects.UpdateAsync(dataObject);

                var selected = new List<Feature>();
                for (var i = 0; i < viewModel.Selections.Count; i++)
                {
                    if (viewModel.Selections[i])
                    {
                        var fid = viewModel.SelectionsIds[i];
                        var feature = await _features.FindByIdAsync(fid);
                        selected.Add(feature);
                    }
                }
                await _dataObjectService.SetFeaturesAsync(dataObject, selected);

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

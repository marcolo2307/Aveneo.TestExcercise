using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Aveneo.TestExcercise.ApplicationCore;
using AutoMapper;
using Aveneo.TestExcercise.Web.ViewModels;

namespace Aveneo.TestExcercise.Web.Controllers
{
    public class FeaturesController : Controller
    {
        private IRepository<Feature> _features;
        private IMapper _mapper { get; }

        public FeaturesController(
            IRepository<Feature> features,
            IMapper mapper)
        {
            _features = features;
            _mapper = mapper;
        }

        // GET: Features
        public async Task<IActionResult> Index()
        {
            var features = await _features.GetAllAsync();
            var viewModels = _mapper.Map<ICollection<FeatureDetailsViewModel>>(features);

            return View(viewModels);
        }

        // GET: Features
        public async Task<IActionResult> Grid()
        {
            var features = await _features.GetAllAsync();
            var viewModels = _mapper.Map<ICollection<FeatureDetailsViewModel>>(features);

            return View(viewModels);
        }

        // GET: Features/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var feature = await _features.FindByIdAsync(id.Value);
            if (feature == null)
                return NotFound();

            var viewModel = _mapper.Map<FeatureDetailsViewModel>(feature);

            return View(viewModel);
        }

        // GET: Features/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Features/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeatureEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var feature = _mapper.Map<Feature>(viewModel);

                await _features.CreateAsync(feature);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Features/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var feature = await _features.FindByIdAsync(id.Value);
            if (feature == null)
                return NotFound();

            var viewModel = _mapper.Map<FeatureEditViewModel>(feature);

            return View(viewModel);
        }

        // POST: Features/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FeatureEditViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var feature = await _features.FindByIdAsync(id);

                _mapper.Map(viewModel, feature);

                await _features.UpdateAsync(feature);

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Features/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var feature = await _features.FindByIdAsync(id.Value);
            if (feature == null)
                return NotFound();

            var viewModel = _mapper.Map<FeatureDetailsViewModel>(feature);

            return View(viewModel);
        }

        // POST: Features/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feature = await _features.FindByIdAsync(id);

            if (feature == null)
                return NotFound();

            await _features.DeleteAsync(feature);
            return RedirectToAction(nameof(Index));
        }
    }
}

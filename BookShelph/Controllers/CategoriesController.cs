using AutoMapper;
using BookShelph.Helpers;
using BookShelph.Models;
using BookShelph.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookShelph.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly BookShelphDbContext _context;
        private IMapper _mapper;
        private IProcessFileUpload _fileUpload;
        private string uploadImagePath = "uploads/category/images/";

        public CategoriesController(BookShelphDbContext context, IMapper mapper, IProcessFileUpload fileUpload)
        {
            _context = context;
            _mapper = mapper;
            _fileUpload = fileUpload;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_TablePartial", categories);
            }

            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            CategoryCreateViewModel viewModel = new CategoryCreateViewModel();
            return PartialView("_CreatePartial", viewModel);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageFile,Name,Description,IsActive")] CategoryCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Category category = _mapper.Map<Category>(viewModel);

                var result = _fileUpload.SaveFile(viewModel.ImageFile, uploadImagePath);
                category.Image = result.UniqueFileName;

                _context.Add(category);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }

            return PartialView("_CreatePartial", viewModel);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            CategoryEditViewModel viewModel = _mapper.Map<CategoryEditViewModel>(category);

            return PartialView("_EditPartial", viewModel);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExistingImage,ImageFile,Name,Description,IsActive")] CategoryEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Category category = _mapper.Map<Category>(viewModel);

                    if (viewModel.ImageFile != null)
                    {
                        _fileUpload.DeleteFile(viewModel.ExistingImage, uploadImagePath);

                        var result = _fileUpload.SaveFile(viewModel.ImageFile, uploadImagePath);
                        category.Image = result.UniqueFileName;
                    }

                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }

            return PartialView("_EditPartial", viewModel);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }


            return PartialView("_DeletePartial", category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            _fileUpload.DeleteFile(category.Image, uploadImagePath);

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}

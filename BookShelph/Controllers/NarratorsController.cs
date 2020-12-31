using AutoMapper;
using BookShelph.Helpers;
using BookShelph.Models;
using BookShelph.ViewModels.Narrators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookShelph.Controllers
{
    public class NarratorsController : Controller
    {
        private readonly BookShelphDbContext _context;
        private IMapper _mapper;
        private IProcessFileUpload _fileUpload;
        private string uploadImagePath = "uploads/narrator/images/";


        public NarratorsController(BookShelphDbContext context, IMapper mapper, IProcessFileUpload fileUpload)
        {
            _context = context;
            _mapper = mapper;
            _fileUpload = fileUpload;
        }

        // GET: Narrators
        public async Task<IActionResult> Index()
        {
            var narrators = await _context.Narrators.ToListAsync();
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_TablePartial", narrators);
            }

            return View(narrators);
        }

        // GET: Narrators/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narrator = await _context.Narrators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (narrator == null)
            {
                return NotFound();
            }

            return View(narrator);
        }

        // GET: Narrators/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");

            NarratorCreateViewModel viewModel = new NarratorCreateViewModel();
            return PartialView("_CreatePartial", viewModel);
        }

        // POST: Narrators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExistingImage,ImageFile,FirstName,LastName,OtherName,GenderId,EmailAddress,PhoneNumber,HouseAddress,IsActive")] NarratorCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Narrator narrator = _mapper.Map<Narrator>(viewModel);

                var result = _fileUpload.SaveFile(viewModel.ImageFile, uploadImagePath);
                narrator.Image = result.UniqueFileName;

                _context.Add(narrator);

                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");

            return PartialView("_CreatePartial", viewModel);
        }

        // GET: Narrators/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narrator = await _context.Narrators.FindAsync(id);
            if (narrator == null)
            {
                return NotFound();
            }

            NarratorEditViewModel viewModel = _mapper.Map<NarratorEditViewModel>(narrator);

            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");
            return PartialView("_EditPartial", viewModel);
        }

        // POST: Narrators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ImageFile,FirstName,LastName,OtherName,GenderId,EmailAddress,PhoneNumber,HouseAddress,IsActive")] NarratorEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Narrator narrator = _mapper.Map<Narrator>(viewModel);

                    if (viewModel.ImageFile != null)
                    {
                        _fileUpload.DeleteFile(viewModel.ExistingImage, uploadImagePath);

                        var result = _fileUpload.SaveFile(viewModel.ImageFile, uploadImagePath);
                        narrator.Image = result.UniqueFileName;
                    }

                    _context.Update(narrator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarratorExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }


            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");

            return PartialView("_EditPartial", viewModel);
        }

        // GET: Narrators/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narrator = await _context.Narrators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (narrator == null)
            {
                return NotFound();
            }

            return PartialView("_DeletePartial", narrator);
        }

        // POST: Narrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var narrator = await _context.Narrators.FindAsync(id);

            _fileUpload.DeleteFile(narrator.Image, uploadImagePath);

            _context.Narrators.Remove(narrator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NarratorExists(long id)
        {
            return _context.Narrators.Any(e => e.Id == id);
        }
    }
}

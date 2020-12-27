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


        public NarratorsController(BookShelphDbContext context, IMapper mapper, IProcessFileUpload fileUpload)
        {
            _context = context;
            _mapper = mapper;
            _fileUpload = fileUpload;
        }

        // GET: Narrators
        public async Task<IActionResult> Index()
        {
            return View(await _context.Narrators.ToListAsync());
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
            return View();
        }

        // POST: Narrators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,FirstName,LastName,OtherName,GenderId,EmailAddress,PhoneNumber,HouseAddress,IsActive")] NarratorCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Narrator narrator = _mapper.Map<Narrator>(viewModel);

                narrator.ImagePath = _fileUpload.SaveFile(viewModel.Image, "uploads/narrator/images");

                _context.Add(narrator);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");
            return View(viewModel);
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
            return View(viewModel);
        }

        // POST: Narrators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Image,FirstName,LastName,OtherName,GenderId,EmailAddress,PhoneNumber,HouseAddress,IsActive")] NarratorEditViewModel viewModel)
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
                return RedirectToAction(nameof(Index));
            }


            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");

            return View(viewModel);
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

            return View(narrator);
        }

        // POST: Narrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var narrator = await _context.Narrators.FindAsync(id);
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

using BookShelph.Models;
using BookShelph.ViewModels.Genders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookShelph.Controllers
{
    public class GendersController : Controller
    {
        private readonly BookShelphDbContext _context;

        public GendersController(BookShelphDbContext context)
        {
            _context = context;
        }

        // GET: Genders
        public async Task<IActionResult> Index()
        {
            var genders = await _context.Genders.ToListAsync();
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_TablePartial", genders);
            }

            return View(genders);
        }

        // GET: Genders/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Genders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        // GET: Genders/Create
        public IActionResult Create()
        {
            GenderCreateViewModel viewModel = new GenderCreateViewModel();
            return PartialView("_CreatePartial", viewModel);
        }

        // POST: Genders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] GenderCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dateTime = DateTime.Now;

                Gender gender = new Gender
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    IsActive = true,
                    CreatedAt = dateTime,
                    ModifiedAt = dateTime,
                };


                _context.Add(gender);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreatePartial", viewModel);
        }

        // GET: Genders/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Genders.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }
            return PartialView("_EditPartial", gender);
        }

        // POST: Genders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name,Description,IsActive")] Gender gender)
        {
            if (id != gender.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenderExists(gender.Id))
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
            return PartialView("_EditPartial", gender);
        }

        // GET: Genders/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Genders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gender == null)
            {
                return NotFound();
            }

            return PartialView("_DeletePartial", gender);
        }

        // POST: Genders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var gender = await _context.Genders.FindAsync(id);
            _context.Genders.Remove(gender);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenderExists(short id)
        {
            return _context.Genders.Any(e => e.Id == id);
        }
    }
}

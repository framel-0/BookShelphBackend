using AutoMapper;
using BookShelph.Helpers;
using BookShelph.Models;
using BookShelph.ViewModels.Languages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShelph.Controllers
{
    public class LanguagesController : Controller
    {
        private readonly BookShelphDbContext _context;
        private IMapper _mapper;
        private IProcessFileUpload _fileUpload;
        private string uploadImagePath = "uploads/language/images/";

        public LanguagesController(BookShelphDbContext context, IMapper mapper, IProcessFileUpload fileUpload)
        {
            _context = context;
            _mapper = mapper;
            _fileUpload = fileUpload;
        }

        // GET: Languages
        public async Task<IActionResult> Index()
        {

            var languages = await _context.Languages.ToListAsync();
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_TablePartial", languages);
            }

            return View(languages);
        }

        // GET: Languages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // GET: Languages/Create
        public IActionResult Create()
        {
            LanguageCreateViewModel viewModel = new LanguageCreateViewModel();
            return PartialView("_CreatePartial", viewModel);
        }

        // POST: Languages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageFile,Name,Description,IsActive")] LanguageCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Language language = _mapper.Map<Language>(viewModel);

                var result = _fileUpload.SaveFile(viewModel.ImageFile, uploadImagePath);
                language.Image = result.UniqueFileName;

                _context.Add(language);
                await _context.SaveChangesAsync();
                CreateNotification("Contact saved!");
            }

            return PartialView("_CreatePartial", viewModel);
        }

        // GET: Languages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages.FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }

            LanguageEditViewModel viewModel = _mapper.Map<LanguageEditViewModel>(language);

            return PartialView("_EditPartial", viewModel);
        }

        // POST: Languages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExistingImage,ImageFile,Name,Description,IsActive")] LanguageEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Language language = _mapper.Map<Language>(viewModel);

                    if (viewModel.ImageFile != null)
                    {
                        _fileUpload.DeleteFile(viewModel.ExistingImage, uploadImagePath);

                        var result = _fileUpload.SaveFile(viewModel.ImageFile, uploadImagePath);
                        language.Image = result.UniqueFileName;
                    }

                    _context.Update(language);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageExists(viewModel.Id))
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

        // GET: Languages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var language = await _context.Languages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return NotFound();
            }

            return PartialView("_DeletePartial", language);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var language = await _context.Languages.FindAsync(id);

            _fileUpload.DeleteFile(language.Image, uploadImagePath);

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        private void CreateNotification(string message)
        {
            TempData.TryGetValue("Notifications", out object value);
            var notifications = value as List<string> ?? new List<string>();
            notifications.Add(message);
            TempData["Notifications"] = notifications;
        }

        public IActionResult Notifications()
        {
            TempData.TryGetValue("Notifications", out object value);
            var notifications = value as IEnumerable<string> ?? Enumerable.Empty<string>();
            return PartialView("_NotificationsPartial", notifications);
        }

        private bool LanguageExists(int id)
        {
            return _context.Languages.Any(e => e.Id == id);
        }
    }
}

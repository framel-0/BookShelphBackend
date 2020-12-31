using AutoMapper;
using BookShelph.Helpers;
using BookShelph.Models;
using BookShelph.ViewModels.Authors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookShelph.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly BookShelphDbContext _context;
        private IMapper _mapper;
        private IProcessFileUpload _fileUpload;
        private string uploadImagePath = "uploads/author/images/";

        public AuthorsController(BookShelphDbContext context, IMapper mapper, IProcessFileUpload fileUpload)
        {
            _context = context;
            _mapper = mapper;
            _fileUpload = fileUpload;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var authors = await _context.Authors.ToListAsync();
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_TablePartial", authors);
            }

            return View(authors);
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");
            AuthorCreateViewModel viewModel = new AuthorCreateViewModel();
            return PartialView("_CreatePartial", viewModel);
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExistingImage,ImageFile,FirstName,LastName,MiddleName,GenderId,EmailAddress,Email,PhoneNumber")] AuthorCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Author author = _mapper.Map<Author>(viewModel);

                var result = _fileUpload.SaveFile(viewModel.ImageFile, uploadImagePath);
                author.Image = result.UniqueFileName;

                _context.Add(author);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }

            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");
            return PartialView("_CreatePartial", viewModel);

        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            AuthorEditViewModel viewModel = _mapper.Map<AuthorEditViewModel>(author);

            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");

            return PartialView("_EditPartial", viewModel);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ImageFile,FirstName,LastName,MiddleName,GenderId,Address,EmailAddress,PhoneNumber,IsActive")] AuthorEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Author author = _mapper.Map<Author>(viewModel);

                    if (viewModel.ImageFile != null)
                    {
                        _fileUpload.DeleteFile(viewModel.ExistingImage, uploadImagePath);

                        var result = _fileUpload.SaveFile(viewModel.ImageFile, uploadImagePath);
                        author.Image = result.UniqueFileName;
                    }

                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(viewModel.Id))
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
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name");
            return PartialView("_EditPartial", viewModel);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return PartialView("_DeletePartial", author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task DeleteConfirmed(long id)
        {
            var author = await _context.Authors.FindAsync(id);

            _fileUpload.DeleteFile(author.Image, uploadImagePath);

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(long id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}

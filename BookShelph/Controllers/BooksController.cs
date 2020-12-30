using AutoMapper;
using BookShelph.Helpers;
using BookShelph.Models;
using BookShelph.ViewModels.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookShelph.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookShelphDbContext _context;
        private IMapper _mapper;
        private IProcessFileUpload _fileUpload;
        private string uploadImagePath = "uploads/books/images/";

        public BooksController(BookShelphDbContext context, IMapper mapper, IProcessFileUpload fileUpload)
        {
            _context = context;
            _mapper = mapper;
            _fileUpload = fileUpload;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var bookShelphDbContext = _context.Books
                .Include(b => b.Audio)
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Language)
                .Include(b => b.Narrator)
                .Include(b => b.Publisher);
            return View(await bookShelphDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Audio)
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Language)
                .Include(b => b.Narrator)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            var authors = _context.Authors.Select(s =>
            new
            {
                Id = s.Id,
                FullName = string.Format("{0} {1}", s.FirstName, s.LastName)
            });

            var narrators = _context.Narrators.Select(s =>
            new
            {
                Id = s.Id,
                FullName = string.Format("{0} {1}", s.FirstName, s.LastName)
            });

            ViewData["AudioId"] = new SelectList(_context.AudioFiles, "Id", "Name");
            ViewData["AuthorId"] = new SelectList(authors, "Id", "FullName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
            ViewData["NarratorId"] = new SelectList(narrators, "Id", "FullName");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name");

            BookCreateViewModel viewModel = new BookCreateViewModel();
            return PartialView("_CreatePartial", viewModel);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExistingCoverImage,CoverImageFile,Title,Description,ReleaseDate,CategoryId,PublisherId,AuthorId,LanguageId,NarratorId,AudioId")] BookCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Book book = _mapper.Map<Book>(viewModel);

                var result = _fileUpload.SaveFile(viewModel.CoverImageFile, uploadImagePath);
                book.CoverImage = result.FileName;

                _context.Add(book);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            var authors = _context.Authors.Select(s =>
           new
           {
               Id = s.Id,
               FullName = string.Format("{0} {1}", s.FirstName, s.LastName)
           });

            var narrators = _context.Narrators.Select(s =>
            new
            {
                Id = s.Id,
                FullName = string.Format("{0} {1}", s.FirstName, s.LastName)
            });

            ViewData["AudioId"] = new SelectList(_context.AudioFiles, "Id", "Name");
            ViewData["AuthorId"] = new SelectList(authors, "Id", "FullName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
            ViewData["NarratorId"] = new SelectList(narrators, "Id", "FullName");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name");

            return PartialView("_CreatePartial", viewModel);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            BookEditViewModel viewModel = _mapper.Map<BookEditViewModel>(book);
            var authors = _context.Authors.Select(s =>
           new
           {
               Id = s.Id,
               FullName = string.Format("{0} {1}", s.FirstName, s.LastName)
           });

            var narrators = _context.Narrators.Select(s =>
            new
            {
                Id = s.Id,
                FullName = string.Format("{0} {1}", s.FirstName, s.LastName)
            });

            ViewData["AudioId"] = new SelectList(_context.AudioFiles, "Id", "Name");
            ViewData["AuthorId"] = new SelectList(authors, "Id", "FullName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
            ViewData["NarratorId"] = new SelectList(narrators, "Id", "FullName");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name");
            return PartialView("_EditPartial", viewModel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CoverImage,Title,Description,ReleaseDate,CategoryId,PublisherId,AuthorId,LanguageId,NarratorId,AudioId,IsActive")] BookEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Book book = _mapper.Map<Book>(viewModel);

                    if (viewModel.CoverImageFile != null)
                    {
                        _fileUpload.DeleteFile(viewModel.ExistingCoverImage, uploadImagePath);

                        var result = _fileUpload.SaveFile(viewModel.CoverImageFile, uploadImagePath);
                        book.CoverImage = result.UniqueFileName;
                    }

                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(viewModel.Id))
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
            var authors = _context.Authors.Select(s =>
           new
           {
               Id = s.Id,
               FullName = string.Format("{0} {1}", s.FirstName, s.LastName)
           });

            var narrators = _context.Narrators.Select(s =>
            new
            {
                Id = s.Id,
                FullName = string.Format("{0} {1}", s.FirstName, s.LastName)
            });

            ViewData["AudioId"] = new SelectList(_context.AudioFiles, "Id", "Name");
            ViewData["AuthorId"] = new SelectList(authors, "Id", "FullName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
            ViewData["NarratorId"] = new SelectList(narrators, "Id", "FullName");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name");
            return PartialView("_EditPartial", viewModel);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Audio)
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.Language)
                .Include(b => b.Narrator)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            _fileUpload.DeleteFile(book.CoverImage, uploadImagePath);

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(long id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}

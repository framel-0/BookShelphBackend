using AutoMapper;
using BookShelph.Helpers;
using BookShelph.Models;
using BookShelph.ViewModels.Publishers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookShelph.Controllers
{
    public class PublishersController : Controller
    {
        private readonly BookShelphDbContext _context;
        private IMapper _mapper;
        private IProcessFileUpload _fileUpload;
        private string uploadImagePath = "uploads/publisher/images/";

        public PublishersController(BookShelphDbContext context, IMapper mapper, IProcessFileUpload fileUpload)
        {
            _context = context;
            _mapper = mapper;
            _fileUpload = fileUpload;
        }

        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            var publishers = await _context.Publishers.ToListAsync();
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_TablePartial", publishers);
            }

            return View(publishers);
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            PublisherCreateViewModel viewModel = new PublisherCreateViewModel();
            return PartialView("_CreatePartial", viewModel);
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExistingImage,ImageFile,Name,Description,IsActive")] PublisherCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Publisher publisher = _mapper.Map<Publisher>(viewModel);

                var result = _fileUpload.SaveFile(viewModel.ImageFile, uploadImagePath);
                publisher.Image = result.UniqueFileName;

                _context.Add(publisher);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreatePartial", viewModel);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            PublisherEditViewModel viewModel = _mapper.Map<PublisherEditViewModel>(publisher);

            return PartialView("_EditPartial", viewModel);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ImageFile,Name,Description,IsActive")] PublisherEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Publisher publisher = _mapper.Map<Publisher>(viewModel);

                    if (viewModel.ImageFile != null)
                    {
                        _fileUpload.DeleteFile(viewModel.ExistingImage, uploadImagePath);

                        var result = _fileUpload.SaveFile(viewModel.ImageFile, uploadImagePath);
                        publisher.Image = result.UniqueFileName;
                    }

                    _context.Update(publisher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return PartialView("_EditPartial", viewModel);
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }

            _fileUpload.DeleteFile(publisher.Image, uploadImagePath);

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(long id)
        {
            return _context.Publishers.Any(e => e.Id == id);
        }
    }
}

using AutoMapper;
using BookShelph.Helpers;
using BookShelph.Models;
using BookShelph.ViewModels.AudioFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookShelph.Controllers
{
    public class AudioFilesController : Controller
    {
        private readonly BookShelphDbContext _context;
        private IMapper _mapper;
        private IProcessFileUpload _fileUpload;
        private string uploadAudioPath = "uploads/audio_file/audios/";

        public AudioFilesController(BookShelphDbContext context, IMapper mapper, IProcessFileUpload fileUpload)
        {
            _context = context;
            _mapper = mapper;
            _fileUpload = fileUpload;
        }

        // GET: AudioFiles
        public async Task<IActionResult> Index()
        {
            var audioFiles = await _context.AudioFiles.ToListAsync();
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_TablePartial", audioFiles);
            }

            return View(audioFiles);
        }

        // GET: AudioFiles/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioFile = await _context.AudioFiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audioFile == null)
            {
                return NotFound();
            }

            return View(audioFile);
        }

        // GET: AudioFiles/Create
        public IActionResult Create()
        {
            AudioFileCreateViewModel viewModel = new AudioFileCreateViewModel();
            return PartialView("_CreatePartial", viewModel);
        }

        // POST: AudioFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AudioFile,Description,Duration")] AudioFileCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                AudioFile audioFile = _mapper.Map<AudioFile>(viewModel);
                var result = _fileUpload.SaveFile(viewModel.AudioFile, uploadAudioPath);
                audioFile.Name = result.FileName;
                audioFile.FileSize = result.FileSize;
                audioFile.FilePath = result.UniqueFileName;

                _context.Add(audioFile);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreatePartial", viewModel);
        }

        // GET: AudioFiles/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioFile = await _context.AudioFiles.FindAsync(id);
            if (audioFile == null)
            {
                return NotFound();
            }
            return View(audioFile);
        }

        // POST: AudioFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Description,FileSize,FilePath,Duration,NormalizedName,CreatedBy,CreatedAt,ModifiedBy,ModifiedAt,IsActive")] AudioFile audioFile)
        {
            if (id != audioFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(audioFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AudioFileExists(audioFile.Id))
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
            return View(audioFile);
        }

        // GET: AudioFiles/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audioFile = await _context.AudioFiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audioFile == null)
            {
                return NotFound();
            }

            return PartialView("_DeletePartial", audioFile);
        }

        // POST: AudioFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var audioFile = await _context.AudioFiles.FindAsync(id);

            _fileUpload.DeleteFile(audioFile.FilePath, uploadAudioPath);

            _context.AudioFiles.Remove(audioFile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AudioFileExists(long id)
        {
            return _context.AudioFiles.Any(e => e.Id == id);
        }
    }
}

using AutoMapper;
using BookShelph.Dtos.Languages;
using BookShelph.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShelph.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly BookShelphDbContext _context;
        private IMapper _mapper;
        public LanguagesController(BookShelphDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/LanguagesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageDto>>> GetLanguages()
        {
            List<Language> languages = await _context.Languages.ToListAsync();
            List<LanguageDto> languagesDto = _mapper.Map<List<Language>, List<LanguageDto>>(languages);

            return languagesDto;
        }

        // GET: api/LanguagesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageDto>> GetLanguage(int id)
        {
            var language = await _context.Languages.FindAsync(id);

            if (language == null)
            {
                return NotFound();
            }

            LanguageDto languageDto = _mapper.Map<LanguageDto>(language);

            return languageDto;
        }

    }
}

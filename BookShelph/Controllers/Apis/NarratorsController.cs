using AutoMapper;
using BookShelph.Dtos.Narrators;
using BookShelph.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShelph.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class NarratorsController : ControllerBase
    {
        private readonly BookShelphDbContext _context;
        private IMapper _mapper;
        public NarratorsController(BookShelphDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/NarratorsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NarratorDto>>> GetNarrators()
        {
            List<Narrator> narrators = await _context.Narrators.ToListAsync();
            List<NarratorDto> narratorsDto = _mapper.Map<List<Narrator>, List<NarratorDto>>(narrators);

            return narratorsDto;
        }

        // GET: api/NarratorsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NarratorDto>> GetNarrator(long id)
        {
            var narrator = await _context.Narrators.FindAsync(id);

            if (narrator == null)
            {
                return NotFound();
            }

            NarratorDto narratorDto = _mapper.Map<NarratorDto>(narrator);

            return narratorDto;
        }

    }
}

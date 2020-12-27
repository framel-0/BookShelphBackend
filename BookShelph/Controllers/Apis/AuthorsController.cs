using AutoMapper;
using BookShelph.Dtos.Authors;
using BookShelph.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShelph.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookShelphDbContext _context;
        private IMapper _mapper;

        public AuthorsController(BookShelphDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/AuthorsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {

            List<Author> authors = await _context.Authors.ToListAsync();
            List<AuthorDto> authorsDto = _mapper.Map<List<Author>, List<AuthorDto>>(authors);

            return authorsDto;

        }

        // GET: api/AuthorsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(long id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            AuthorDto authorDto = _mapper.Map<AuthorDto>(author);

            return authorDto;
        }

    }
}

using AutoMapper;
using BookShelph.Dtos.Publishers;
using BookShelph.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShelph.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly BookShelphDbContext _context;
        private IMapper _mapper;

        public PublishersController(BookShelphDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/PublishersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDto>>> GetPublishers()
        {
            List<Publisher> publishers = await _context.Publishers.ToListAsync();
            List<PublisherDto> publishersDto = _mapper.Map<List<Publisher>, List<PublisherDto>>(publishers);

            return publishersDto;
        }

        // GET: api/PublishersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDto>> GetPublisher(long id)
        {
            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            PublisherDto publisherDto = _mapper.Map<PublisherDto>(publisher);

            return publisherDto;
        }

    }
}

using AutoMapper;
using BookShelph.Dtos.Category;
using BookShelph.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShelph.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly BookShelphDbContext _context;
        private IMapper _mapper;
        public CategoriesController(BookShelphDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/CategoriesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            List<Category> categorys = await _context.Categories.ToListAsync();
            List<CategoryDto> categorysDto = _mapper.Map<List<Category>, List<CategoryDto>>(categorys);

            return categorysDto;
        }

        // GET: api/CategoriesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }

    }
}

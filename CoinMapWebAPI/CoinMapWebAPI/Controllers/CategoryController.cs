using AutoMapper;
using CoinMapWebAPI.BLL.Services.Intefaces;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.Models.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public CategoryController(IMapper mapper, ICategoryService categoryService, IUserService userService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _userService = userService;
        }

        [HttpPost("{categoryName}")]
        public async Task<IActionResult> CreateCategory(string categoryName)
        {
            var currentUser = await _userService.GetCurrentUserAsync(User);

            var category = await _categoryService.CreateCategoryAsync(categoryName, currentUser.Id);

            return CreatedAtAction(nameof(GetCategory), new { categoryId = category.Id }, _mapper.Map<Cate>(category));
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);

            return Ok(_mapper.Map<CategoryResponseDTO>(category));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            List<Category> categories = await _categoryService.GetAllCategoriesAsync();

            return Ok(_mapper.Map<List<CategoryResponseDTO>>(categories));
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> EditCategory(int categoryId, string categoryName)
        {
            await _categoryService.EditCategoryAsync(categoryName, categoryId);

            return Ok();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            await _categoryService.DeleteCategoryAsync(categoryId);

            return Ok();
        }

        [HttpGet("{categoryId}/venues")]
        public async Task<IActionResult> GetAllVenuesFromCategory(int categoryId)
        {
            List<Venue> venues = await _categoryService.GetAllVenuesFromCategoryAsync(categoryId);

            return Ok(_mapper.Map<List<CategoryResponseDTO>>(venues));
        }
    }
}

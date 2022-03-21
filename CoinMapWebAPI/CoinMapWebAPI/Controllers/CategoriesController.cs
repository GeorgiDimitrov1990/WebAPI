using AutoMapper;
using CoinMapWebAPI.BLL.Services.Intefaces;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.Models.DTO.Requests.Category;
using CoinMapWebAPI.Models.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Controllers
{
    [Route("api/category")]
    [Authorize]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public CategoriesController(IMapper mapper, ICategoryService categoryService, IUserService userService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDTO category)
        {
            var currentUser = await _userService.GetCurrentUserAsync(User);

            var createdCategory = await _categoryService.CreateCategoryAsync(category.CategoryName, currentUser.Id);

            return CreatedAtAction(nameof(GetCategory), new { categoryId = createdCategory.Id }, _mapper.Map<CategoryResponseDTO>(createdCategory));
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

        [Authorize(Roles = "Admin")]
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> EditCategory(int categoryId, EditCategoryRequestDTO category)
        {
            await _categoryService.EditCategoryAsync(category.CategoryName, categoryId);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            await _categoryService.DeleteCategoryAsync(categoryId);

            return Ok();
        }

        [HttpGet("{categoryId}/venues")]
        public async Task<IActionResult> GetVenues(int categoryId)
        {
            List<Venue> venues = await _categoryService.GetAllVenuesFromCategoryAsync(categoryId);

            return Ok(_mapper.Map<List<VenueResponseDTO>>(venues));
        }
    }
}

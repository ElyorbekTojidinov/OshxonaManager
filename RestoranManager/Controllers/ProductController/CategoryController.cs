using Aplication.DTOs.Category;
using Aplication.Interfaces.InterfacesProduct;
using Aplication.Models;
using Domain.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoranManager.Filter;

namespace RestoranManager.Controllers.ProductController;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ApiControllerBase<Categories>
{
    private readonly ICategoryRepository _categoryService;
    private readonly IProductRepository _productService;
    public CategoryController(ICategoryRepository categoryServise, IProductRepository productService)
    {
        _categoryService = categoryServise;
        _productService = productService;
    }

    [HttpGet]
   
    [Route("[action]"), Authorize(Roles = "GetById")]
    public async Task<ActionResult<ResponseCore<CategoryGetDTO>>> GetByIdCategory(int id)
    {
        Categories? obj = await _categoryService.GetByIdAsync(id);
        if (obj == null)
        {
            return NotFound(new ResponseCore<Categories?>(false, id + " not found!"));
        }
        CategoryGetDTO mappedCategory = _mapper.Map<CategoryGetDTO>(obj);
        return Ok(new ResponseCore<CategoryGetDTO?>(mappedCategory));
    }


    [HttpGet]
    [AuthorizationFilter]
    [Route("[action]"), Authorize(Roles = "GetAll")]
    public async Task<ActionResult<ResponseCore<CategoryGetDTO>>> GetAllCategory()
    {
        IEnumerable<Categories> category = await _categoryService.GetAsync(x => true);
        IEnumerable<CategoryGetDTO> mappedCategory = _mapper.Map<IEnumerable<CategoryGetDTO>>(category);
        return Ok(new ResponseCore<IEnumerable<CategoryGetDTO>>(mappedCategory));
    }

    [HttpPost]
    [Route("[action]"), Authorize(Roles = "Create")]
    [ActionModelValidation]
    public async Task<ActionResult<ResponseCore<CategoryGetDTO>>> CreateCategory([FromBody] CategoryCreateDTO category)
    {
        Categories? mappedController = _mapper.Map<Categories>(category);
        var validationResult = _validator.Validate(mappedController);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
        }
       
        mappedController = await _categoryService.CreateAsync(mappedController);
        var res = _mapper.Map<CategoryGetDTO>(mappedController);
        return Ok(new ResponseCore<CategoryGetDTO>(res));

    }

    [HttpPut]
    [Route("[action]"), Authorize(Roles = "Update")]
    [ActionModelValidation]
    public async Task<ActionResult<ResponseCore<CategoryGetDTO>>> UpdateCategory([FromBody] CategoryUpdateDTO category)
    {
        Categories? mappedCategory = _mapper.Map<Categories>(category);
        var validationResult = _validator.Validate(mappedCategory);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<Categories>(false, validationResult.Errors));
        }
       
        mappedCategory = await _categoryService.UpdateAsync(mappedCategory);
        if (mappedCategory != null)
            return Ok(new ResponseCore<CategoryGetDTO>(_mapper.Map<CategoryGetDTO>(mappedCategory)));
        return BadRequest(new ResponseCore<Categories>(false, category + " not found"));

    }


    [HttpDelete]
    [Route("[action]"), Authorize(Roles = "Delete")]
    public async Task<ActionResult<ResponseCore<CategoryGetDTO>>> DeleteCategory([FromQuery] int id)
    {
        return await _categoryService.DeleteAsync(id) ?
                 Ok(new ResponseCore<bool>(true))
               : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
    }

}

using Aplication.DTOs.Order;
using Aplication.DTOs.Product;
using Aplication.Interfaces.InterfacesProduct;
using Aplication.Models;
using Domain.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoranManager.Filter;

namespace RestoranManager.Controllers.ProductController;

[Route("api/[controller]")]
[ApiController, Authorize]
public class ProductController : ApiControllerBase<Products>
{
    private readonly IProductRepository _productService;
    private readonly ICategoryRepository _categoryService;
    public ProductController(IProductRepository productService, ICategoryRepository categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetById")]
    public async Task<ActionResult<ResponseCore<ProductGetDTO>>> GetByIdProduct(int id)
    {
        Products? obj = await _productService.GetByIdAsync(id);
        if (obj == null)
        {
            return NotFound(new ResponseCore<Products?>(false, id + " not found!"));
        }
        ProductGetDTO mappedProduct = _mapper.Map<ProductGetDTO>(obj);
        return Ok(new ResponseCore<ProductGetDTO?>(mappedProduct));
    }

    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetAll")]
    public async Task<ActionResult<ResponseCore<ProductGetDTO>>> GetAllProduct()
    {
        IEnumerable<Products> order = await _productService.GetAsync(x => true);

        IEnumerable<ProductGetDTO> mappedProduct = _mapper.Map<IEnumerable<ProductGetDTO>>(order);

        return Ok(new ResponseCore<IEnumerable<ProductGetDTO>>(mappedProduct));

    }

    [HttpPost]
    [Route("[action]"), Authorize(Roles = "Create")]
    [ActionModelValidation] 
    public async Task<ActionResult<ResponseCore<ProductGetDTO>>> CreateProduct([FromBody] ProductCreateDTO product)
    {
        Products mappedProduct = _mapper.Map<Products>(product);
        var validationResult = _validator.Validate(mappedProduct);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
        }
        ///     shu yerda hato bor 
        mappedProduct = await _productService.CreateAsync(mappedProduct);
        var res = _mapper.Map<ProductGetDTO>(mappedProduct);
        return Ok(new ResponseCore<ProductGetDTO>(res));
    }

    [HttpPut]
    [Route("[action]"), Authorize(Roles = "Update")]
    [ActionModelValidation]
    public async Task<ActionResult<ResponseCore<ProductGetDTO>>> UpdateProduct([FromBody] ProductUpdateDTO product)
    {
        Products mappedProduct = _mapper.Map<Products>(product);
        var validationResult = _validator.Validate(mappedProduct);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
        }
        ///     shu yerda hato bor 
        mappedProduct = await _productService.UpdateAsync(mappedProduct);
        var res = _mapper.Map<OrderGetDTO>(mappedProduct);
        return Ok(new ResponseCore<OrderGetDTO>(res));
    }


    [HttpDelete]
    [Route("[action]"), Authorize(Roles = "Delete")]
    public async Task<ActionResult<ResponseCore<ProductGetDTO>>> DeleteProduct([FromQuery] int id)
    {
        return await _categoryService.DeleteAsync(id) ?
                Ok(new ResponseCore<bool>(true))
              : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
    }

}


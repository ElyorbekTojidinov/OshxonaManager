using Aplication.DTOs.Order;
using Aplication.DTOs.Roles;
using Aplication.Interfaces.InterfacesProduct;
using Aplication.Models;
using Domain.Models.Models;
using Domain.Models.ModelsJwt;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoranManager.Filter;
using System.Data;

namespace RestoranManager.Controllers.ProductController;

[Route("api/[controller]")]
[ApiController, Authorize]

public class OrderController : ApiControllerBase<Orders>
{
    private readonly IOrdersRepository _orderService;
    private readonly IProductRepository _productRepository;
    public OrderController(IOrdersRepository orderService, IProductRepository productRepository)
    {
        _orderService = orderService;
        _productRepository = productRepository;
    }


    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetById")]
    public async Task<ActionResult<ResponseCore<OrderGetDTO>>> GetByIdOrder(int id)
    {
        Orders? obj = await _orderService.GetByIdAsync(id);
        if (obj == null)
        {
            return NotFound(new ResponseCore<Orders?>(false, id + " not found!"));
        }
        OrderGetDTO mappedOrder = _mapper.Map<OrderGetDTO>(obj);
        return Ok(new ResponseCore<OrderGetDTO?>(mappedOrder));
    }


    [HttpPost]
    [Route("[action]"), Authorize(Roles = "Create")]
    [ActionModelValidation]
    public async Task<ActionResult<ResponseCore<OrderGetDTO>>> CreateOrder([FromBody] OrderCreateDTO order)
    {
        Orders mappedOrder = _mapper.Map<Orders>(order);
        var validationResult = _validator.Validate(mappedOrder);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
        }
        mappedOrder.Products = new List<Products>();
        foreach (var item in order.Products)
        {
            Products? permissions = await _productRepository.GetByIdAsync(item);
            if (order != null)
                mappedOrder.Products.Add(permissions);
            else return BadRequest(new ResponseCore<string>(false, item + " Id not found"));
        }
        mappedOrder = await _orderService.CreateAsync(mappedOrder);
        var res = _mapper.Map<OrderGetDTO>(mappedOrder);
        return Ok(new ResponseCore<OrderGetDTO>(res));
    }


    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetAll")]
    public async Task<ActionResult<ResponseCore<OrderGetDTO>>> GetAllOrders()
    {
        IEnumerable<Orders> order =await _orderService.GetAsync(x => true);

        IEnumerable<OrderGetDTO> mappedOrder = _mapper.Map<IEnumerable<OrderGetDTO>>(order);

        return Ok(new ResponseCore<IEnumerable<OrderGetDTO>>(mappedOrder));

    }

    [HttpPut]
    [Route("[action]"), Authorize(Roles = "Update")]
    [ActionModelValidation]
    public async Task<ActionResult<ResponseCore<OrderGetDTO>>> UpdateOrder([FromBody] OrderUpdateDTO order)
    {
        Orders? mappedOrder = _mapper.Map<Orders>(order);
        var validationResult = _validator.Validate(mappedOrder);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<Orders>(false, validationResult.Errors));
        }
        mappedOrder.Products = new List<Products>();
        foreach (var item in order.Products)
        {
            Products? products =await _productRepository.GetByIdAsync(item);
            if (order != null)
                mappedOrder.Products.Add(products);
            else return BadRequest(new ResponseCore<string>(false, item + " Id not found"));
        }
        mappedOrder = await _orderService.UpdateAsync(mappedOrder);
        if (mappedOrder != null)
            return Ok(new ResponseCore<OrderGetDTO>(_mapper.Map<OrderGetDTO>(mappedOrder)));
        return BadRequest(new ResponseCore<Orders>(false, order + " not found"));
    }


    [HttpDelete]
    [Route("[action]"), Authorize(Roles = "Delete")]
    public async Task<ActionResult<ResponseCore<OrderGetDTO>>> DeleteOrder([FromQuery] int id)
    {
        return await _orderService.DeleteAsync(id) ?
                Ok(new ResponseCore<bool>(true))
              : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
    }

}

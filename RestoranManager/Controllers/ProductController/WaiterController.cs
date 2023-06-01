using Aplication.DTOs.Waiter;
using Aplication.Interfaces.InterfacesProduct;
using Aplication.Models;
using Domain.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoranManager.Filter;

namespace RestoranManager.Controllers.ProductController;

[Route("api/[controller]")]
[ApiController, Authorize]
public class WaiterController : ApiControllerBase<Waiter>
{
    private readonly IWaiterRepository _waiterService;
    private readonly IOrdersRepository _ordersRepository;
    public WaiterController(IWaiterRepository waiterService, IOrdersRepository ordersRepository)
    {
        _waiterService = waiterService;
        _ordersRepository = ordersRepository;
    }

    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetById")]

    public async Task<ActionResult<ResponseCore<WaiterGetDTO>>> GetByIdWaiter(int id)
    {
        Waiter? obj = await _waiterService.GetByIdAsync(id);
        if (obj == null)
        {
            return NotFound(new ResponseCore<Waiter?>(false, id + " not found!"));
        }
        WaiterGetDTO mappedWaiter = _mapper.Map<WaiterGetDTO>(obj);
        return Ok(new ResponseCore<WaiterGetDTO?>(mappedWaiter));
    }


    [HttpGet]
    [Route("[action]"), Authorize(Roles = "GetAll")]
    public async Task<ActionResult<ResponseCore<WaiterGetDTO>>> GetAllWaiter()
    {
        IEnumerable<Waiter> waiter =await _waiterService.GetAsync(x => true);

        IEnumerable<WaiterGetDTO> mappedWaiter = _mapper.Map<IEnumerable<WaiterGetDTO>>(waiter);

        return Ok(new ResponseCore<IEnumerable<WaiterGetDTO>>(mappedWaiter));

    }


    [HttpPost]
    [Route("[action]"), Authorize(Roles = "Create")]
    [ActionModelValidation]
    public async Task<ActionResult<ResponseCore<WaiterGetDTO>>> CreateWaiter([FromBody] WaiterCreateDTO waiter)
    {
        Waiter mappedWaiter = _mapper.Map<Waiter>(waiter);
        var validationResult = _validator.Validate(mappedWaiter);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
        }
        mappedWaiter.Orders = new List<Orders>();
        foreach (var item in waiter.Orders)
        {
            Orders? orders = await _ordersRepository.GetByIdAsync(item);
            if (waiter != null)
                mappedWaiter.Orders.Add(orders);
            else return BadRequest(new ResponseCore<string>(false, item + " Id not found"));
        }
        mappedWaiter = await _waiterService.CreateAsync(mappedWaiter);
        var res = _mapper.Map<WaiterGetDTO>(mappedWaiter);
        return Ok(new ResponseCore<WaiterGetDTO>(res));
    }


    [HttpPut]
    [Route("[action]"), Authorize(Roles = "Update")]
    [ActionModelValidation]
    public async Task<ActionResult<ResponseCore<WaiterGetDTO>>> UpdateWaiter([FromBody] WaiterUpdateDTO waiter)
    {
        Waiter mappedWaiter = _mapper.Map<Waiter>(waiter);
        var validationResult = _validator.Validate(mappedWaiter);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
        }
        mappedWaiter.Orders = new List<Orders>();
        foreach (var item in waiter.Orders)
        {
            Orders? orders = await _ordersRepository.GetByIdAsync(item);
            if (waiter != null)
                mappedWaiter.Orders.Add(orders);
            else return BadRequest(new ResponseCore<string>(false, item + " Id not found"));
        }
        mappedWaiter = await _waiterService.UpdateAsync(mappedWaiter);
        var res = _mapper.Map<WaiterGetDTO>(mappedWaiter);
        return Ok(new ResponseCore<WaiterGetDTO>(res));
    }

    [HttpDelete]
    [Route("[action]"), Authorize(Roles = "Delete")]
    public async Task<ActionResult<ResponseCore<WaiterGetDTO>>> DeleteWaiter([FromQuery] int id)
    {
        return await _waiterService.DeleteAsync(id) ?
                 Ok(new ResponseCore<bool>(true))
               : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
    }

}
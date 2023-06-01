using Aplication.DTOs.Order;
using Aplication.DTOs.Roles;
using Aplication.Interfaces.InterfacesJwt;
using Aplication.Models;
using Aplication.Services.ServicesJwt;
using Domain.Models.ModelsJwt;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using RestoranManager.Filter;
using System.Data;

namespace RestoranManager.Controllers.JwtController;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ApiControllerBase<Roles>
{
    private readonly IRoleRepository _repository;
  
    private readonly ILogger<RoleController> _logger;
    private readonly IPermissionRepository _permissionRepository;
    public RoleController( IRoleRepository repository, ILogger<RoleController> logger, IPermissionRepository permissionRepository)
    {
       
        _repository = repository;
        _logger = logger;
        _permissionRepository = permissionRepository;
    }

    [HttpPost]
    [Route("[action]")]
    [ActionModelValidation]
    public async Task<ActionResult<ResponseCore<RoleGetDTO>>> Create([FromBody] RoleCreateDTO role)
    {
        Roles? mappedRole = _mapper.Map<Roles>(role);
        var validationResult = _validator.Validate(mappedRole); 
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
        }
        mappedRole.Permission = new List<Permission>();
        foreach (var item in role.Permissions)
        {
            Permission? permissions = await _permissionRepository.GetByIdAsync(item);
            if (role != null)
                mappedRole.Permission.Add(permissions);
            else return BadRequest(new ResponseCore<string>(false, item + " Id not found"));
        }
        mappedRole = await _repository.CreateAsync(mappedRole);
        var res = _mapper.Map<RoleGetDTO>(mappedRole);
        return Ok(new ResponseCore<RoleGetDTO>(res));
    }

    [HttpGet]
    [Route("[action]{id}")]
    public async Task<ActionResult<ResponseCore<RoleGetDTO>>> GetById(int id)
    {
        Roles? obj = await _repository.GetByIdAsync(id);
        if (obj == null)
        {
            return NotFound(new ResponseCore<Roles?>(false, id + " not found!"));
        }
        RoleGetDTO mappedRole = _mapper.Map<RoleGetDTO>(obj);
        return Ok(new ResponseCore<RoleGetDTO?>(mappedRole));
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<ActionResult<ResponseCore<RoleGetDTO>>> GetAll()
    {
        IEnumerable<Roles> role = await _repository.GetAsync(x => true);

        IEnumerable<RoleGetDTO> mappedRoles = _mapper.Map<IEnumerable<RoleGetDTO>>(role);

        return Ok(new ResponseCore<IEnumerable<RoleGetDTO>>(mappedRoles));
    }

    [HttpPut]
    [Route("[action]")]
    [ActionModelValidation]
    public async Task<ActionResult<ResponseCore<RoleGetDTO>>> Update([FromBody] RoleUpdateDTO role)
    {
        Roles? mappedRoles = _mapper.Map<Roles>(role);
        var validationResult = _validator.Validate(mappedRoles);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<Roles>(false, validationResult.Errors));
        }
        mappedRoles.Permission = new List<Permission>();
        foreach (var item in role.Permissions)
        {
            Permission? permissions = await _permissionRepository.GetByIdAsync(item);
            if (role != null)
                mappedRoles.Permission.Add(permissions);
            else return BadRequest(new ResponseCore<string>(false, item + " Id not found"));
        }
        mappedRoles = await _repository.UpdateAsync(mappedRoles);
        if (mappedRoles != null)
            return Ok(new ResponseCore<RoleGetDTO>(_mapper.Map<RoleGetDTO>(mappedRoles)));
        return BadRequest(new ResponseCore<Roles>(false, role + " not found"));
    }

    [HttpDelete]
    [Route("[action]{id}")]
    public async Task<ActionResult<ResponseCore<RoleGetDTO>>> Delete(int id)
    {
        return await _repository.DeleteAsync(id) ?
               Ok(new ResponseCore<bool>(true))
             : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
    }
}

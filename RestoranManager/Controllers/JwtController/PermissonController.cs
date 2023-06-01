using Aplication.DTOs.Permission;
using Aplication.Interfaces.InterfacesJwt;
using Aplication.Models;
using Domain.Models.ModelsJwt;
using Microsoft.AspNetCore.Mvc;
using RestoranManager.Filter;

namespace RestoranManager.Controllers.JwtController;

[Route("api/[controller]")]
[ApiController]
public class PermissonController : ApiControllerBase<Permission>
{
    private readonly IPermissionRepository _permissionService;
   
    public PermissonController(IPermissionRepository permissionService)
    {
        _permissionService = permissionService;
       
    }

    [HttpGet("GetAllPermissions")]
    public async Task<ActionResult<ResponseCore<PermissionGetDTO>>> GetAllPermissionsAsync()
    {
        IEnumerable<Permission> permissions = await _permissionService.GetAsync(x => true);
        IEnumerable<PermissionGetDTO> mappedPermission = _mapper.Map<IEnumerable<PermissionGetDTO>>(permissions);
        return Ok(new ResponseCore<IEnumerable<PermissionGetDTO>>(mappedPermission));
    }


    [HttpPost("AddPermission")]
    [ActionModelValidation]
    public async Task<ActionResult<ResponseCore<PermissionGetDTO>>> CreatePermissionAsync([FromForm] PermissionCreateDTO permission)
    {

        Permission? mappedPermission = _mapper.Map<Permission>(permission);
        var validationResult = _validator.Validate(mappedPermission);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
        }
        mappedPermission = await _permissionService.CreateAsync(mappedPermission);
        var res = _mapper.Map<PermissionGetDTO>(mappedPermission);
        return Ok(new ResponseCore<PermissionGetDTO>(res));
    }


    [HttpPut("UpdatePermission")]
    [ActionModelValidation]
    public async Task<ActionResult<ResponseCore<PermissionGetDTO>>> UpdatePermissionAsync([FromForm] PermissionUpdateDTO permission)
    {
        Permission? permission1 = _mapper.Map<Permission>(permission);
        var validateResult = _validator.Validate(permission1);
        if (!validateResult.IsValid)
        {
            return BadRequest(new ResponseCore<object>(false, validateResult.Errors));
        }
       permission1 = await _permissionService.UpdateAsync(permission1);
        var res = _mapper.Map<PermissionGetDTO>(permission1);
        return Ok(new ResponseCore<PermissionGetDTO>(res));
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseCore<PermissionGetDTO>>> DeletePermissionAsync(int id)
    {
        return await _permissionService.DeleteAsync(id) ?
                  Ok(new ResponseCore<bool>(true))
                : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
    }


    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<ResponseCore<PermissionGetDTO>>> GetByIdPermissionAsync(int id)
    {
        Permission? obj = await _permissionService.GetByIdAsync(id);
        if(obj == null)
        {
            return NotFound(new ResponseCore<Permission?>(false, id + " not found!"));
        }
        PermissionGetDTO mappedPermission = _mapper.Map<PermissionGetDTO>(obj);
        return Ok(new ResponseCore<PermissionGetDTO?>(mappedPermission));
    }

}

using Aplication.DTOs.User;
using Aplication.Interfaces.InterfacesJwt;
using Aplication.Models;
using Domain.Models.JwtNotCreateDb;
using Domain.Models.ModelsJwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoranManager.Filter;
using System.Security.Claims;

namespace RestoranManager.Controllers.JwtController;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class UserController : ApiControllerBase<Users>
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserController> _logger;
    public UserController(IJwtService jwtService,
        IUserRepository userRepository, IUserRefreshTokenRepository userRefreshTokenRepository,
        IConfiguration configuration, ILogger<UserController> logger)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
        _userRefreshTokenRepository = userRefreshTokenRepository;
        _configuration = configuration;
        _logger = logger;
       
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("[action]")]
    public async Task<IActionResult> RefreshToken(Token token)
    {
       _logger.LogInformation($"{nameof(RefreshToken)}");
        var principal = _jwtService.GetPrincipalFromExpiredToken(token.AccessToken);
        var name = principal.FindFirstValue(ClaimTypes.Name);
        IQueryable<Users> users = await _userRepository.GetAsync(x => x.UserName == name);

        var user = users.First();

        var credential = new UserCredentials 
        { 
            UserName = user.UserName, 
            Password = user.Password 
        };

        UserRefreshToken savedRefreshToken = await _userRefreshTokenRepository.GetSavedRefreshTokens(name, token.RefreshToken);
        if (savedRefreshToken == null && savedRefreshToken.RefreshToken != token.RefreshToken)
        {
            return Unauthorized("Invalid input");
        }
        if (savedRefreshToken.Expiretime < DateTime.UtcNow)
        {
            return Unauthorized(" time limit of the token has expired !");
        }

        var newJwt = await _jwtService.GenerateTokenAsync(user);
        if (newJwt == null)
        {
            return Unauthorized("Invalid input");
        }
        int min = 4;
        if (int.TryParse(_configuration["JWT:RefreshTokenExpiresTime"], out int _min))
        {
            min = _min;
        }
        UserRefreshToken refreshToken = new()
        {
            RefreshToken = newJwt.RefreshToken,
            UserName = name,
            Expiretime = DateTime.UtcNow.AddMinutes(min)
        };
        bool IsDeleted = await _userRefreshTokenRepository.DeleteUserRefreshTokens(name, token.RefreshToken);
        if (IsDeleted)
        {
            await _userRefreshTokenRepository.AddUserRefreshTokens(refreshToken);
        }
        else
        {
            return BadRequest();
        }
        return Ok(newJwt);

    }



    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromForm] UserCredentials userCredentials)
    {
        _logger.LogInformation("Login is called");
        string hashedPsw = await _userRepository.ComputeHashAsync(userCredentials.Password);
        Users user = (await _userRepository.GetAsync(x => x.UserName == userCredentials.UserName && x.Password == hashedPsw)).SingleOrDefault(); 
        
       
        int min = 4;
        if (int.TryParse(_configuration["JWT:RefreshTokenExpiresTime"], out int _min))
        {
            min = _min;
        }
        var token = await _jwtService.GenerateTokenAsync(user);
        var refreshToken = new UserRefreshToken
        {
            UserName = userCredentials.UserName,
            Expiretime = DateTime.UtcNow.AddMinutes(min),
            RefreshToken = token.RefreshToken
        };
        await _userRefreshTokenRepository.UpdateUserRefreshToken(refreshToken);
        return Ok(token);

    }



    [HttpPost]
    [Route("[action]")]
    [ActionModelValidation]
    public async Task<ActionResult<ResponseCore<UserGetDTO>>> Create([FromBody] UserCreateDTO user)
    {
        Users? mappedUser = _mapper.Map<Users>(user);
        var validationResult = _validator.Validate(mappedUser);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
        }
        mappedUser = await _userRepository.CreateAsync(mappedUser);
        var res = _mapper.Map<UserGetDTO>(mappedUser);
        return Ok(new ResponseCore<UserGetDTO>(res));
    }

    [HttpGet]
    [Route("[action]{id}")]
    public async Task<ActionResult<ResponseCore<UserGetDTO>>> GetById(int Id)
    {
        Users? obj = await _userRepository.GetByIdAsync(Id);
        if (obj == null)
        {
            return NotFound(new ResponseCore<Permission?>(false, Id + " not found!"));
        }
        UserGetDTO mappedUser = _mapper.Map<UserGetDTO>(obj);
        return Ok(new ResponseCore<UserGetDTO?>(mappedUser));
    }



    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<ResponseCore<UserGetDTO>>> GetAll()
    {
        IEnumerable<Users> users = await _userRepository.GetAsync(x => true);
        IEnumerable<UserGetDTO> mappedUser = _mapper.Map<IEnumerable<UserGetDTO>>(users);
        return Ok(new ResponseCore<IEnumerable<UserGetDTO>>(mappedUser));

    }


    [HttpPut]
    [Route("[action]")]
    [ActionModelValidation]
    public async Task<ActionResult<ResponseCore<UserGetDTO>>> Update([FromBody] UserUpdateDTO user)
    {
        Users? mappedUser = _mapper.Map<Users>(user);
        var validationResult = _validator.Validate(mappedUser);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
        }
        mappedUser = await _userRepository.UpdateAsync(mappedUser);
        var res = _mapper.Map<UserGetDTO>(mappedUser);
        return Ok(new ResponseCore<UserGetDTO>(res));

    }



    [HttpDelete]
    [Route("[action]{id}")]
    public async Task<ActionResult<ResponseCore<UserGetDTO>>> Delete(int id)
    {
        return await _userRepository.DeleteAsync(id) ?
                   Ok(new ResponseCore<bool>(true))
                 : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));
    }
}


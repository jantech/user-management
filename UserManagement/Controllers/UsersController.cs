using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Core.Entities;
using UserManagement.Core.ViewModels;
using UserManagement.Helpers;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController: ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IUserService userService, 
                                IMapper mapper,
                                IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Register([FromBody]RegisterViewModel registerViewModel)
        {
            var regUser = _mapper.Map<User>(registerViewModel);
            
            try
            {
                var regResp = _userService.Create(regUser, registerViewModel.Password);
                var regOut = _mapper.Map<UserViewModel>(regResp);                
                return Ok(new ApiResponse { Status = true, Message = "User successfully registered", Data = regOut });
            }
            catch(CustomException ex)
            {
                return BadRequest(new ApiResponse { Status = false, Message = ex.Message, Data = null });
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Authenticate([FromBody]AuthenticateViewModel authenticateViewModel)
        {
            var user = _userService.Authenticate(authenticateViewModel.Username, authenticateViewModel.Password);

            if (user == null)
            {
                return BadRequest(new ApiResponse { Status = false, Message = "Username or password is incorrect", Data = null });
            }

            // jwt token generation
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var resp = _mapper.Map<UserTokenViewModel>(user);
            resp.Token = tokenString;

            return Ok(new ApiResponse { Status = true, Message = "User Authentication successful!", Data = resp });
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetUserById(id);
            var output = _mapper.Map<UserViewModel>(user);

            if (user == null)
            {
                return BadRequest(new ApiResponse { Status = false, Message = "User detail not found", Data = null });
            }

            return Ok(new ApiResponse { Status = true, Message = "User details!", Data = output });
        }

    }
}

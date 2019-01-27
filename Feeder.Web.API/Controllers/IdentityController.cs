namespace Feeder.Web.API.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Feeder.API.Models.User;
    using Feeder.Core.User;
    using Feeder.Web.API.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    [Authorize]
    [ApiController]
    [Route("api/Identity")]
    public class IdentityController : Controller
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;

        public IdentityController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserLoginInputModel model)
        {
            var user = await _userRepository.GetUserByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest(new { message = "Username or Password is incorrect." });
            }

            var authenticatedUser = _userRepository.AuthenticateAsync(user, model.Password);

            if (authenticatedUser == null)
            {
                return BadRequest(new { message = "Email or Password is incorrect." });
            }

            var token = GenerateToken(authenticatedUser);

            var userLoginOutputModel = new UserLoginOutputModel()
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token
            };

            return Ok(userLoginOutputModel);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterationInputModel model)
        {
            var userByEmail = await _userRepository.GetUserByEmailAsync(model.Email);

            if (userByEmail != null)
            {
                return BadRequest();
            }

            var user = Core.User.User.New(model.FirstName, model.LastName, model.Email);

            await _userRepository.RegisterAsync(user, model.Password);

            return Ok(user.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync()
        {
            if (!long.TryParse(User.UserId(), out long userId))
            {
                return BadRequest(new { message = "Invalid User Id." });
            }

            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userOutputModel = new UserOutputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return Ok(userOutputModel);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["IdentitySettings:SecurityKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(FeederClaimTypes.UserId, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(_config["IdentitySettings:Isuser"],
                _config["IdentitySettings:Isuser"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
using CarBuddy.Application.Models.DTOs;
using CarBuddy.Application.Services;
using CarBuddy.Application.Validators;
using CarBuddy.Domain.Models;
using CarBuddy.WebApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CarBuddy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly UserValidator _userValidator;
        private readonly CredentialsValidator _credentialsValidator;
        private readonly EmailService _emailService;

        private readonly string _imagesRoot;
        private readonly string _emailTemplatePath;

        public AuthController(
            AuthService authService,
            UserService userService,
            UserValidator userValidator,
            CredentialsValidator credentialsValidator,
            EmailService emailService,
            IWebHostEnvironment hostEnvironment)
        {
            _authService = authService;
            _userService = userService;
            _userValidator = userValidator;
            _credentialsValidator = credentialsValidator;
            _emailService = emailService;
            _imagesRoot = hostEnvironment.WebRootPath + "\\Images\\";
            _emailTemplatePath = hostEnvironment.WebRootPath + "\\Templates/Email.html";
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register()
        {
            var form = Request.Form;

            if (!int.TryParse(form["age"].ToString(), out int age))
                return BadRequest(new { Error = "Age is invalid." });

            var user = new User(
                form["firstName"].ToString(),
                form["lastName"].ToString(),
                form["email"].ToString(),
                form["password"].ToString(),
                age);

            var validationResult = _userValidator.Validate(user);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(r => new { Error = r.ErrorMessage }));

            var result = _authService.Register(user);

            if (result.HasError)
                return Problem(result.Message, statusCode: result.StatusCode);

            var emailResult = await _emailService.Send(new RegisterDto(result), _emailTemplatePath);

            if (emailResult.HasError)
            {
                _userService.DeleteUser(user);
                return Problem("An error occurred while sending mail", statusCode: emailResult.StatusCode);
            }

            if (form.Files.Any())
            {
                var file = new UploadedFile(_imagesRoot);
                var fullPath = await file.Upload(form.Files[0]);
                _userService.UploadPhoto(user, fullPath);
            }

            return Ok(new { Id = result.GetProperty("Id") });
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Credentials credentials)
        {
            var validationResult = _credentialsValidator.Validate(credentials);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(r => new { Error = r.ErrorMessage }));

            var result = _authService.Login(credentials);

            return result.HasError
                ? Problem(result.Message, statusCode: result.StatusCode)
                : Ok(result.Content);
        }

        [HttpPost]
        [Route("confirm-email")]
        public IActionResult ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var result = _authService.ConfirmEmail(userId, token);

            return result.HasError
                ? Problem(result.Message, statusCode: result.StatusCode)
                : Ok(result.Content);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using AllocellaAPI.DTOs.Auth;
using AllocellaAPI.Services.Auth;

/*
      Quackeyikz is here again, of course. Who do you think wrote this code?!

      This is THE Controller that interract with the FE for handling POST requests: Login and Register.
      Basically, accepting the [FromBody] request, attempt to register / login with the email.

      If fails, returns error with the code (i hope i didnt miss label them, just incase i put 2 exceptional throwing in register)
      If sucessful, returns the result variable with a 200 code.
*/

namespace AllocellaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
      private readonly IAuthService _authService;
      // Using ASP.NET built in logger, for easier logging: LogInformation, LogError, LogWarning, etc.
      private readonly ILogger<AuthController> _logger;

      public AuthController(IAuthService authService, ILogger<AuthController> logger)
      {
            _authService = authService;
            _logger = logger;
      }

      [HttpPost("register")]
      public async Task<IActionResult> Register([FromBody] RegisterAccountRequest request)
      {
            try
            {
                  _logger.LogInformation("Registration attempt for email: {Email}", request.Email);

                  var result = await _authService.RegisterAsync(request);

                  _logger.LogInformation("User registered successfully: {Email}", request.Email);

                  return Ok(result);
            }
            catch (InvalidOperationException e)
            {
                  _logger.LogWarning("Registration failed for {Email}", request.Email);
                  return BadRequest(new { message = e.Message });
            }
            // Just incase, i put generic exception
            catch (Exception e)
            {
                  _logger.LogError(e, "Unexpected error during registration for {Email}", request.Email);
                  return StatusCode(500, new { mesage = "An unexpected error occured in the system. Please try again later" });
            }
      }

      [HttpPost("login")]
      public async Task<IActionResult> Login([FromBody] LoginRequest request)
      {
            try
            {
                  _logger.LogInformation("Login attempt for email: {Email}", request.Email);

                  var result = await _authService.LoginAsync(request);

                  if (result == null)
                  {
                        _logger.LogWarning("Login failed for {Email}: Invalid credentials", request.Email);
                        return Unauthorized(new { message = "Invalid email or password" });
                  }

                  _logger.LogInformation("User logged in successfully: {Email}", request.Email);

                  return Ok(result);
            }
            catch (Exception e)
            {
                  _logger.LogError(e, "Unexpected error during login for {Email}", request.Email);
                  return StatusCode(500, new { message = "An unexpected error in the system. Please try again later."});
            }
      }
}
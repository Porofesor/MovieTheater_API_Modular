//using Identity.IdentityCore.JWT.Infrastructure.Infrastructure;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Identity.IdentityCore.JWT.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class UsersController : ControllerBase
//    {
//        private readonly RegisterUsers _registerUsers;
//        private readonly LoginUsers _loginUsers;

//        public UsersController(RegisterUsers registerUsers, LoginUsers loginUsers)
//        {
//            _registerUsers = registerUsers;
//            _loginUsers = loginUsers;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterUsers.Request request)
//        {
//            try
//            {
//                var user = await _registerUsers.Handle(request);
//                return Ok(new { Message = "Registration successful, please verify your email." });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { Message = ex.Message });
//            }
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginUsers.Request request)
//        {
//            try
//            {
//                string token = await _loginUsers.Handle(request);
//                return Ok(new { Token = token });
//            }
//            catch (Exception ex)
//            {
//                return Unauthorized(new { Message = ex.Message });
//            }
//        }

//        [HttpGet("verify-email")]
//        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
//        {
//            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailVerificationToken == token);

//            if (user == null)
//            {
//                return BadRequest("Invalid or expired verification token.");
//            }

//            user.IsEmailVerified = true;
//            user.EmailVerificationToken = null; // Clear the token after successful verification
//            await _context.SaveChangesAsync();

//            return Ok("Email verified successfully.");
//        }
//    }
//}
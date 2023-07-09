using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyAuthJWT.Config;
using PolicyAuthJWT.Config.Auth.JwtService;
using PolicyAuthJWT.Models.Login;

namespace PolicyAuthJWT.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IJwtService _jwtService;
        public AccountController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }
        [Route("/login")]
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        [Route("/login")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromForm] LoginModel loginModel)
        {
            var user = Enums.Users.FirstOrDefault(x => x.Username == loginModel.UserName
                && x.Password == loginModel.Password);

            if (user == null)
            {
                return View();
            }

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Username, user.Roles, loginModel.KeepLoggedIn);

            //// Store the token securely (e.g., in local storage)
            HttpContext.Response.Cookies.Append("jwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new { Token = token });
        }
    }
}

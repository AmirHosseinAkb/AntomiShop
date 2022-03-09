using Antomi.Core.DTOs.User;
using Antomi.Core.Generators;
using Antomi.Core.Security;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Antomi.Core.Convertors;
using Antomi.Core.Senders;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace AntomiShop.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IViewRenderService _viewRenderService;
        public AccountController(IUserService userService,IViewRenderService viewRenderService)
        {
            _userService = userService; 
            _viewRenderService = viewRenderService;
        }

        [Route("/RegisterAndLogin")]
        public IActionResult RegisterAndLogin()
        {
            return View();
        }
        
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterAndLoginViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View("_RegisterArea", register);
            }
            if (_userService.IsExistEmail(register.Email))
            {
                ModelState.AddModelError("Email", "این ایمیل از قبل وجود دارد");
                return View("_RegisterArea", register);
            }
            if (register.Password != register.Repassword)
            {
                ModelState.AddModelError("RePassword", "تکرار رمز عبور صحیح نمی باشد");
                return View("_RegisterArea", register);
            }
            User user = new User()
            {
                Email = register.Email,
                Password = PasswordHasher.HashPasswordMD5(register.Password),
                ActiveCode = NameGenerator.GenerateUniqName(),
                AvatarName = "Default.png",
            };
            //Send Activation Email

            var body = _viewRenderService.RenderToStringAsync("_ActivationEmail", user);
            SendEmail.Send(user.Email, "فعالسازی حساب کاربری", body);

            //Add User
            _userService.AddUser(user);
            return View("_SuccessRegister", user);
        }
        
        [Route("ActiveAccount/{activeCode}")]
        public IActionResult ActiveAccount(string activeCode)
        {
            ViewBag.IsActived = _userService.ActiveUserAccount(activeCode);
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(RegisterAndLoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View("_LoginArea", login);
            }
            var user = _userService.GetUserForLogin(login.Email, login.Password);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.Email),
                        new Claim("AvatarName",user.AvatarName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);
                    ViewBag.LogedIn = true;
                    return View("_LoginArea");
                }
                else
                {
                    ModelState.AddModelError("Email", "لطفا ابتدا حساب کاربری خود را فعال کنید");
                    return View("_LoginArea", login);
                }
            }
            else
            {
                ModelState.AddModelError("Email", "کاربری با این مشخصات وجود ندارد");
                return View("_LoginArea", login);
            }
        }
    }
}

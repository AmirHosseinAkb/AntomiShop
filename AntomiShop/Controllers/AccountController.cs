using Antomi.Core.DTOs.User;
using Antomi.Core.Generators;
using Antomi.Core.Security;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Antomi.Core.Convertors;
using Antomi.Core.Senders;

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
                return View("RegisterAndLogin", register);
            }
            if (_userService.IsExistEmail(register.Email))
            {
                ModelState.AddModelError("Email", "این ایمیل از قبل وجود دارد");
                return View("RegisterAndLogin", register);
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
    }
}

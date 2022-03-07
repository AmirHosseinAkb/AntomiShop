using Microsoft.AspNetCore.Mvc;

namespace AntomiShop.Controllers
{
    public class AccountController : Controller
    {

        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}

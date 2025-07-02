using Microsoft.AspNetCore.Mvc;
using C__MVC_Assessment.Data;
using Microsoft.EntityFrameworkCore;

namespace C__MVC_Assessment.Controllers
{
    public class HomeController : BaseController
    {
             public HomeController(Context context) : base(context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

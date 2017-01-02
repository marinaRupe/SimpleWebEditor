using Microsoft.AspNetCore.Mvc;

namespace SimpleWebEditorApplication.Controllers
{
    //[Authorize]
    public class PageEditorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
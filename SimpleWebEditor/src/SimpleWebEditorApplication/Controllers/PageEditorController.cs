using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleWebEditorApplication.Models.PageEditorViewModels;

namespace SimpleWebEditorApplication.Controllers
{
    [Authorize]
    public class PageEditorController : Controller
    {
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                WorkPagePath = "'../html/templates/template7.html'",
                PublishedPagePath = "'../html/templates/template8.html'"
            };
            return View(model);
        }
    }
}
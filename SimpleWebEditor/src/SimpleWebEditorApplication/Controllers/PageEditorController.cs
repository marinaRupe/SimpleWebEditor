using System;
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
                PublishedPagePath = "'../html/templates/template8.html'",
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SavePage(string html)
        {
            //TODO: save html string to user's work page
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult PublishPage(string html)
        {
            //TODO: save html string to user's published page
            return RedirectToAction("Index");
        }
    }
}
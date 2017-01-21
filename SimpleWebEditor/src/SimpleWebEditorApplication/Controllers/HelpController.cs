using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleWebEditorApplication.Core.Models;
using SimpleWebEditorApplication.Models.HelpViewModels;

namespace SimpleWebEditorApplication.Controllers
{
    [Authorize]
    public class HelpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendRequest(SendRequestViewModel model)
        {
            var description = model.Description;

            return RedirectToAction("Index");
        }
    }
}
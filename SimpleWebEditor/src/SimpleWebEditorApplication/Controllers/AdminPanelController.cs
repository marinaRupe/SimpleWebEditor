using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleWebEditorApplication.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("UserListPanel");
        }

        public IActionResult UserListPanel()
        {
            return View();
        }

        public IActionResult PromoteUserPanel()
        {
            return View();
        }

        public IActionResult DeletePagePanel()
        {
            return View();
        }

        public IActionResult UserRequestListPanel()
        {
            return View();
        }

        public IActionResult CreateNewUserPanel()
        {
            return View();
        }
    }
}
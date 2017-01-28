using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleWebEditorApplication.Models.AdminPanelViewModels;

namespace SimpleWebEditorApplication.Controllers
{
    [Authorize]
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            //TODO: send list of users
            return RedirectToAction("UserListPanel");
        }

        public IActionResult UserListPanel()
        {
            var userList = new List<UserListPanelViewModel>();

            //TODO: foreach (list of users)
            for (var i = 0; i < 10; i++)
            {
                var user = new UserListPanelViewModel
                {
                    Username = "marince",
                    FirstName = "Marina",
                    LastName = "Rupe",
                    Role = "user",
                    Link = "/UserPages/PublishedPages/marince.html"
                };
                userList.Add(user);
            }
            
            return View(userList);
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
            var requestList = new List<UserRequestListPanelViewModel>();

            //TODO: foreach (list of requests)
            for (var i = 0; i < 10; i++)
            {
                var request = new UserRequestListPanelViewModel
                {
                    Username = "marince6",
                    Request = "Hej pomogni mi"
                };
                requestList.Add(request);
            }
            return View(requestList);
        }

        public IActionResult CreateNewUserPanel()
        {
            return View();
        }

       
    }
}
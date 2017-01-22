using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleWebEditorApplication.Models.ProfileViewModels;

namespace SimpleWebEditorApplication.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IndexViewModel _userData;
        public ProfileController()
        {
            _userData = new IndexViewModel
            {
                Username = "marince6",
                Email = "marina.rupe@fer.hr",
                FirstName = "Marina",
                LastName = "Rupe",
                BirthDate = new DateTime(1995, 12, 6)
            };
        }
        public IActionResult Index()
        {
            return View(_userData);
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            _userData.FirstName = model.FirstName;
            _userData.LastName = model.LastName;
            _userData.BirthDate = model.BirthDate;

            return View("Index", _userData);
        }
    }
}
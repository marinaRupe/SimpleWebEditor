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
                BirthDate = "06.12.1995."
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

            return View("Index", _userData);
        }
    }
}
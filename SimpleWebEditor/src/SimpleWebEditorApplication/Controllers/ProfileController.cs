using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleWebEditorApplication.Core.Interfaces;
using SimpleWebEditorApplication.Core.Models;
using SimpleWebEditorApplication.Models;
using SimpleWebEditorApplication.Models.ProfileViewModels;

namespace SimpleWebEditorApplication.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(IAccountRepository accountRepository, UserManager<ApplicationUser> userManager)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var acc = await GetCurrentUserAccountAsync();
            return View(await CreateIndexViewModel(acc));
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var acc = await GetCurrentUserAccountAsync();

            acc.FirstName = model.FirstName ?? acc.FirstName;
            acc.LastName = model.LastName ?? acc.LastName;
            acc.BirthDate = model.BirthDate.Equals(default(DateTime)) ? acc.BirthDate : model.BirthDate;
            _accountRepository.Update(acc);

            return View("Index", await CreateIndexViewModel(acc));
        }

        private async Task<IndexViewModel> CreateIndexViewModel(Account acc)
        {
            var user = await GetCurrentUserAsync();
            var model = new IndexViewModel
            {
                Username = acc.UserName,
                Email = user.Email,
                FirstName = acc.FirstName,
                LastName = acc.LastName,
                BirthDate = acc.BirthDate.GetValueOrDefault()
            };
            return model;
        }

        private async Task<Account> GetCurrentUserAccountAsync()
        {
            var user = await GetCurrentUserAsync();
            return _accountRepository.Get(user.UserName);
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
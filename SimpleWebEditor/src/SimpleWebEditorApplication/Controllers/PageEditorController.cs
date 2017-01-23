using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleWebEditorApplication.Core.Interfaces;
using SimpleWebEditorApplication.Core.Models;
using SimpleWebEditorApplication.Models;
using SimpleWebEditorApplication.Models.PageEditorViewModels;

namespace SimpleWebEditorApplication.Controllers
{
    [Authorize]
    public class PageEditorController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPageRepository _pageRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public PageEditorController(
            IAccountRepository accountRepository, 
            IPageRepository pageRepository,
            UserManager<ApplicationUser> userManager)
        {
            _accountRepository = accountRepository;
            _pageRepository = pageRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await CreateIndexViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SavePage(string html)
        {
            var acc = await GetCurrentUserAccountAsync();
            _pageRepository.GetByOwner(acc, false).OverwriteFile(html);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> PublishPage(string html)
        {
            var acc = await GetCurrentUserAccountAsync();
            _pageRepository.GetByOwner(acc, true).OverwriteFile(html);
            return RedirectToAction("Index");
        }

        private async Task<IndexViewModel> CreateIndexViewModel()
        {
            var acc = await GetCurrentUserAccountAsync();
            var model = new IndexViewModel
            {
                WorkPagePath = _pageRepository.GetByOwner(acc, false).RequestPagePath(),
                PublishedPagePath = _pageRepository.GetByOwner(acc, true).RequestPagePath()

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
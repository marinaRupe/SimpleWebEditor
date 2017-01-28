using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleWebEditorApplication.Core.Interfaces;
using SimpleWebEditorApplication.Core.Models;
using SimpleWebEditorApplication.Models;
using SimpleWebEditorApplication.Models.HelpViewModels;

namespace SimpleWebEditorApplication.Controllers
{
    [Authorize]
    public class HelpController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRequestRepository _userRequestRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HelpController(
            IAccountRepository accountRepository, 
            IUserRequestRepository userRequestRepository,
            UserManager<ApplicationUser> userManager)
        {
            _accountRepository = accountRepository;
            _userRequestRepository = userRequestRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendRequest(SendRequestViewModel model)
        {
            _userRequestRepository.Add(new UserRequest(await GetCurrentUserAccountAsync(), model.Description));
            return RedirectToAction("Index");
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
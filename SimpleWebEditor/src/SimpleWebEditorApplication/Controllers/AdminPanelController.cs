using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleWebEditorApplication.Core.Interfaces;
using SimpleWebEditorApplication.Models;
using SimpleWebEditorApplication.Models.AdminPanelViewModels;

namespace SimpleWebEditorApplication.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class AdminPanelController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPageRepository _pageRepository;
        private readonly IUserRequestRepository _userRequestRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminPanelController(
            IAccountRepository accountRepository, 
            IPageRepository pageRepository,
            IUserRequestRepository userRequestRepository,
            UserManager<ApplicationUser> userManager)
        {
            _accountRepository = accountRepository;
            _pageRepository = pageRepository;
            _userRequestRepository = userRequestRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("UserListPanel");
        }

        public IActionResult UserListPanel()
        {
            var userList = CreateUserListPanelViewModelList();
            return View(userList);
        }

        private List<UserListPanelViewModel> CreateUserListPanelViewModelList()
        {
            return _accountRepository.GetAll().Select(acc => new UserListPanelViewModel
            {
                Username = acc.UserName,
                FirstName = acc.FirstName,
                LastName = acc.LastName,
                Role = GetUserRoleAsync(acc.UserName).Result,
                Link = _pageRepository.GetByOwner(acc, true).RequestPagePath()
            }).ToList();
        }

        public IActionResult PromoteUserPanel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PromoteUserPanel(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, Startup.ADMIN_ROLE);
                return RedirectToAction("UserListPanel");
            }
            return View();
        }

        public IActionResult DeletePagePanel()
        {
            return View();
        }

        public IActionResult UserRequestListPanel()
        {
            var requestList = CreateUserRequestListPanelViewModelList();
            return View(requestList);
        }

        private List<UserRequestListPanelViewModel> CreateUserRequestListPanelViewModelList()
        {
            return _userRequestRepository.GetAll().Select(ur => new UserRequestListPanelViewModel
            {
                RequestId = ur.Id.ToString(),
                Username = ur.Sender.UserName,
                Request = ur.Description
            }).ToList();
        }

        public IActionResult CreateNewUserPanel()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteUserRequest(string id)
        {
            _userRequestRepository.Remove(new Guid(id));
            return RedirectToAction("UserRequestListPanel");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string username)
        {
            _accountRepository.Remove(username);
            var user = await _userManager.FindByNameAsync(username);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("UserListPanel");
        }

        [HttpGet]
        public IActionResult ConfirmDeleteUser(string username)
        {
            return View("ConfirmDeleteUser", username);
        }

        [HttpPost]
        public IActionResult DeleteUserPage(string username)
        {
            var acc = _accountRepository.Get(username);
            if (acc != null)
            {
                var page = _pageRepository.GetByOwner(acc, true);
                _pageRepository.UpdatePageCode(page.Id, "<html></html>");
                return RedirectToAction("UserListPanel");
            }
            // fail
            return View("DeletePagePanel");
        }

        private async Task<string> GetUserRoleAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.Count == 0 ? "user" : String.Join(String.Empty, roles);
        }
    }
}
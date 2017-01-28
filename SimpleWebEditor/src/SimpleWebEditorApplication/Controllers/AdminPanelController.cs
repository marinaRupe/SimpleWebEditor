using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleWebEditorApplication.Core.Interfaces;
using SimpleWebEditorApplication.Models;
using SimpleWebEditorApplication.Models.AdminPanelViewModels;

namespace SimpleWebEditorApplication.Controllers
{
    [Authorize]
    public class AdminPanelController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPageRepository _pageRepository;
        private readonly IUserRequestRepository _userRequestRepository;

        public AdminPanelController(
            IAccountRepository accountRepository, 
            IPageRepository pageRepository,
            IUserRequestRepository userRequestRepository)
        {
            _accountRepository = accountRepository;
            _pageRepository = pageRepository;
            _userRequestRepository = userRequestRepository;
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
                Role = "blank",
                Link = _pageRepository.GetByOwner(acc, true).RequestPagePath()
            }).ToList();
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
            var requestList = CreateUserRequestListPanelViewModelList();
            return View(requestList);
        }

        private List<UserRequestListPanelViewModel> CreateUserRequestListPanelViewModelList()
        {
            return _userRequestRepository.GetAll().Select(ur => new UserRequestListPanelViewModel
            {
                Username = ur.Sender.UserName,
                Request = ur.Description
            }).ToList();
        }

        public IActionResult CreateNewUserPanel()
        {
            return View();
        }

       
    }
}
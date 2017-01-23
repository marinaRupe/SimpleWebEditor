using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleWebEditorApplication.Core.Interfaces;
using SimpleWebEditorApplication.Models.PageListViewModels;

namespace SimpleWebEditorApplication.Controllers
{
    public class PageListController : Controller
    {
        private readonly IPageRepository _pageRepository;

        public PageListController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public IActionResult Index()
        {
            var list = CreateIndexViewModelList().OrderBy(e => e.Username).ToList();
            return View(list);
        }

        private List<IndexViewModel> CreateIndexViewModelList()
        {
            return _pageRepository.GetPublished().Select(page => new IndexViewModel
            {
                Username = page.Owner.UserName,
                Link = page.RequestPagePath()
            }).ToList();
        }
    }
}
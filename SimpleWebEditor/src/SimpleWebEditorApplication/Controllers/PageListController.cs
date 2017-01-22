using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleWebEditorApplication.Models.PageListViewModels;

namespace SimpleWebEditorApplication.Controllers
{
    public class PageListController : Controller
    {
        public IActionResult Index()
        {

            var pageList = new List<IndexViewModel>();
            // TODO: foreach, sort by username
            for (var i = 0; i < 10; i++)
            {
                var page = new IndexViewModel
                {
                    Username = "marince6",
                    Link = "/Home/Index"
                };

                pageList.Add(page);
            }


            return View(pageList);
        }
    }
}
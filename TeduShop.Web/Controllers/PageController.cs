using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Model.Models;
using TedShop.Data.Infrastructure;
using TeduShop.Service;
using AutoMapper;
using TeduShop.Web.Models;
namespace TeduShop.Web.Controllers
{
    public class PageController : Controller
    {


        IPageService _pageService;
        public PageController(IPageService pageService)
        {
            this._pageService = pageService;
        }
        public ActionResult Index(string alias)
        {

            var page = _pageService.GetByAlias(alias);
            var model = Mapper.Map<Page, PageViewModel>(page);
            return View(model);
        }
    }
}
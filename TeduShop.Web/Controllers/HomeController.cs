﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Model.Models;
using TeduShop.Data;
using TeduShop.Service;
using TeduShop.Web.Models;
using AutoMapper;


namespace TeduShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        ICommonService _commonService;
        IProductService _productService;
        public HomeController(IProductCategoryService productCategoryService,ICommonService commonService,IProductService productService)
        {

            _productCategoryService = productCategoryService;
            _commonService = commonService;
            _productService = productService;
        }
        [OutputCache(Duration =60, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {

            var slideModel = _commonService.GetSlides();
            var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideView;


            var lastestProductModel = _productService.GetLastest(3);
            var topSaleProductModel = _productService.GetHotProduct(3);
            var lastestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            var topSaleProductViewModel  = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);

            homeViewModel.LastestProducts = lastestProductViewModel;
            homeViewModel.TopSaleProducts = topSaleProductViewModel;
            return View(homeViewModel);
        }

        [ChildActionOnly] /*chi de nhung khong goi su dung khi cac phan it su dung hoac luong truy cap nhieu vi du 3600 het khoang thoi gian dok no lay du lieu*/
        [OutputCache(Duration =36000)]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel =  Mapper.Map<Footer,FooterViewModel>(footerModel);
 
            return PartialView(footerViewModel);
        }
    
        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 36000)]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var listproductcategorymode = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);

            return PartialView(listproductcategorymode);
        }

    }
}
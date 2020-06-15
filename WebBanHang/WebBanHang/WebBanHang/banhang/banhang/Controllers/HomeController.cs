using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.D;
using banhang.Models;
using banhang.Common;

namespace banhang.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Slide = new SlideD().ListAll();
            var productd = new ProductD();
            ViewBag.NewProduct = productd.ListNewProduct(4);
            ViewBag.ListFeatureProduct = productd.ListFeatureProduct(4);
            return View();
        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuD().ListByGroupId(1);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new MenuD().ListByGroupId(2);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterD().GetFooter();
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[Common.CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

    }
}
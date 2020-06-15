using Model.D;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace banhang.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductCategoryD();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryD();
                long id = dao.Insert(productcategory);
                if (id > 0)
                {
                    SetAlert("Thêm danh mục sản phẩm thành công", "success");

                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm danh mục sản phẩm không thành công");
                }

            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var productcategory = new ProductCategoryD().ViewDetail(id);
            return View(productcategory);
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryD();

                var result = dao.Update(productcategory);
                if (result)
                {
                    SetAlert("Cập Nhật danh mục sản phẩm thành công", "success");

                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Cập Nhật  danh mục sản phẩm không thành công");
                }
            }
            return View("Index");

        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductCategoryD().Delete(id);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductCategoryD().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
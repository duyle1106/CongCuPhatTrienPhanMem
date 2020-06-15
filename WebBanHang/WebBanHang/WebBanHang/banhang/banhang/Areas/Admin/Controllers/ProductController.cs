using Model.D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using banhang.Common;
using Model.EF;

namespace banhang.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString,int page=1,int pageSize=10)
        {
            var dao = new ProductD();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductD();
                long id = dao.Insert(product);
                if (id > 0)
                {
                    SetAlert("Thêm sản phẩm thành công", "success");

                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sản phẩm không thành công");
                }

            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = new ProductD().ViewDetail(id);
            SetViewBag();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductD();
                var result = dao.Update(product);
                if (result)
                {
                    SetAlert("Cập Nhật sản phẩm thành công", "success");

                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập Nhật  sản phẩm không thành công");
                }
            }
            return View("Index");

        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductD().Delete(id);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductD().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryD();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

    }
}
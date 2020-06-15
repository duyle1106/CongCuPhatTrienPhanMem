using banhang.Common;
using Microsoft.AspNet.Identity;
using Model.D;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace banhang.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        
        public ActionResult Index(string searchString,int page = 1,int pageSize=10)
        {
            var dao = new CategoryD();
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
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryD();
                long id = dao.Insert(category);
                if (id > 0)
                {
                    SetAlert("Thêm category thành công", "success");

                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Category không thành công");
                }
                
            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = new CategoryD().ViewDetail(id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryD();

                var result = dao.Update(category);
                if (result)
                {
                    SetAlert("Cập Nhật category thành công", "success");

                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Cập Nhật không thành công");
                }
            }
            return View("Index");

        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {

            new CategoryD().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CategoryD().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
        
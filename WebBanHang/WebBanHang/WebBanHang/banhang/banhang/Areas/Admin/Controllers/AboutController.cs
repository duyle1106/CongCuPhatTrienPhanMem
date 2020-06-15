using Model.D;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace banhang.Areas.Admin.Controllers
{
    public class AboutController : BaseController
    {
        // GET: Admin/About
        
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new AboutD();
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
        public ActionResult Create(About about)
        {
            if (ModelState.IsValid)
            {
                var dao = new AboutD();
                long id = dao.Insert(about);
                if (id > 0)
                {
                    SetAlert("Thêm  thành công", "success");

                    return RedirectToAction("Index", "About");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm  không thành công");
                }

            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var about = new AboutD().ViewDetail(id);
            return View(about);
        }
        [HttpPost]
        public ActionResult Edit(About about)
        {
            if (ModelState.IsValid)
            {
                var dao = new AboutD();

                var result = dao.Update(about);
                if (result)
                {
                    SetAlert("Cập Nhật  thành công", "success");

                    return RedirectToAction("Index", "About");
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

            new AboutD().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new AboutD().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
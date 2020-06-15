using Model.D;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace banhang.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Admin/Slide
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new SlideD();
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
        public ActionResult Create(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var dao = new SlideD();
                long id = dao.Insert(slide);
                if (id > 0)
                {
                    SetAlert("Thêm  thành công", "success");

                    return RedirectToAction("Index", "Slide");
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
            var slide = new SlideD().ViewDetail(id);
            return View(slide);
        }
        [HttpPost]
        public ActionResult Edit(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var dao = new SlideD();

                var result = dao.Update(slide);
                if (result)
                {
                    SetAlert("Cập Nhật  thành công", "success");

                    return RedirectToAction("Index", "Slide");
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

            new SlideD().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new SlideD().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
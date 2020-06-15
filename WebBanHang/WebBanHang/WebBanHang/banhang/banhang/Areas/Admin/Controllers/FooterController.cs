using Model.D;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace banhang.Areas.Admin.Controllers
{
    public class FooterController : BaseController
    {
        // GET: Admin/Footer
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new FooterD();
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
        [ValidateInput(false)]
        public ActionResult Create(Footer footer)
        {
            if (ModelState.IsValid)
            {
                var dao = new FooterD();
                string id = dao.Insert(footer);
                if(id != null)
                {
                    SetAlert("Thêm  tin tức thành công", "success");

                    return RedirectToAction("Index", "Footer");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Footer không thành công");
                }

            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var dao = new FooterD();
            var footer = dao.GetById(id);
            return View();

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit (Footer footer)
        {
            if (ModelState.IsValid)
            {
                var dao = new FooterD();

                var result = dao.Update(footer);
                if (result)
                {
                    SetAlert("Cập Nhật footer thành công", "success");

                    return RedirectToAction("Index", "footer");
                }
                else
                {
                    ModelState.AddModelError("", "Cập Nhật không thành công");
                }
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            new FooterD().Delete(id);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            var result = new FooterD().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

    }
}
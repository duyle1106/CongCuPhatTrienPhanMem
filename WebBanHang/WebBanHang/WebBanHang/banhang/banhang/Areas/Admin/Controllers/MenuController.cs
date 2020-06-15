using Model.D;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace banhang.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new MenuD();
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
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuD();
                long id = dao.Insert(menu);
                if (id > 0)
                {
                    SetAlert("Thêm  thành công", "success");

                    return RedirectToAction("Index", "Menu");
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
            var menu = new MenuD().ViewDetail(id);
            return View(menu);
        }
        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var dao = new MenuD();

                var result = dao.Update(menu);
                if (result)
                {
                    SetAlert("Cập Nhật  thành công", "success");

                    return RedirectToAction("Index", "Menu");
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

            new MenuD().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new MenuD().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
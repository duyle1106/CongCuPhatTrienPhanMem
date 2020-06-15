using Model.D;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using banhang.Common;
using PagedList;

namespace banhang.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content/
     

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContentD();
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
        [ValidateInput(false)]
        public ActionResult Create(Content content)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentD();
                long id = dao.Insert(content);
                if (id > 0)
                {
                    SetAlert("Thêm  tin tức thành công", "success");

                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Content không thành công");
                }

            }
            SetViewBag(content.CategoryID);
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ContentD();
            var content = dao.GetByID(id);
            SetViewBag(content.CategoryID);
            return View();

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content content)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentD();

                var result = dao.Update(content);
                if (result)
                {
                    SetAlert("Cập Nhật tin tức thành công", "success");

                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Cập Nhật không thành công");
                }
            }
            SetViewBag(content.CategoryID);
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ContentD().Delete(id);

            return RedirectToAction("Index");
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryD();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ContentD().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
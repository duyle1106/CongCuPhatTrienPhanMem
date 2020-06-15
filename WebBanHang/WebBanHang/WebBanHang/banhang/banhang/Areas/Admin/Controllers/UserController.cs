using banhang.Common;
using Model.D;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace banhang.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCredential(RoleID="VIEW_USER")]
        public ActionResult Index(string searchString,int page = 1,int pageSize = 10)
        {
            var dao = new UserD();
            var model = dao.ListAllPaging(searchString,page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.CommonConstants.ADMIN_GROUP] = null;

            return RedirectToAction("Login");
        }
        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HasCredential(RoleID = "ADD_USER")]
        [HttpPost]
        public ActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserD();
                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pas;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm User không thành công");
                }
            }
            
            return View("Index");
            
        }
      
        [HttpGet]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            var user = new UserD().ViewDetail(id);
            SetViewBag();
            return View(user);
        }
      
        [HasCredential(RoleID = "EDIT_USER")]
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserD();
                if(!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMd5Pas;
                }
             
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Sửa user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập Nhật User không thành công");
                }
            }
            return View("Index");

        }
        [HttpDelete]
        [HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new UserD().Delete(id); 

            return RedirectToAction("Index");
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserD().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new UserGroupD();
            ViewBag.GroupID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }


    }
}
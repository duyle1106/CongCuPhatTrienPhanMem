using banhang.Areas.Admin.Models;
using banhang.Common;
using Model.D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace banhang.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        

        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserD();
                var result = dao.login(model.UserName, Encryptor.MD5Hash(model.Password),true);
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    userSession.GroupID = user.GroupID;
                    var listCredentials = dao.GetListCreadential(model.UserName);
                    Session.Add(Common.CommonConstants.SESSION_CREDENTIALS, listCredentials);
                    Session.Add(Common.CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }else if(result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật Khẩu không đúng");
                }
                else if(result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn không có quyền đăng nhập");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng Nhập Không Đúng");
                }
            }
            return View("Index");
        }
        
        
       
    }
}
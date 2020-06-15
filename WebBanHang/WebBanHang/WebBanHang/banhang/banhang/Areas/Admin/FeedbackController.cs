using banhang.Areas.Admin.Controllers;
using Model.D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace banhang.Areas.Admin
{
    public class FeedbackController : BaseController
    {
        // GET: Admin/Feedback
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new FeedbackD();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
    }
}
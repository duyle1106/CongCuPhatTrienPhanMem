
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.D;



namespace banhang.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryD().ListAll();
            return PartialView(model);
        }
        public JsonResult ListName(string q)
        {
            var data = new ProductD().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Category(long cateId,int page = 1 , int pageSize = 1)
        {
            var category = new CategoryD().ViewDetail(cateId);
            ViewBag.Category = category;
            int totalRecord = 0;
            var model = new ProductD().ListByCategoryId(cateId,ref totalRecord,page,pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }
        public ActionResult Search(string keyword, int page = 1, int pageSize = 1)
        {
            int totalRecord = 0;
            var model = new ProductD().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        public ActionResult Detail(long id)
        {
            var product = new ProductD().ViewDetail(id);
            ViewBag.Category = new ProductCategoryD().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProducts = new ProductD().ListRelatedProducts(id);
            return View(product);
        }
        
 
    }
}
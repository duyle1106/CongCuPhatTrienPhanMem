using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.ViewModel;
using System.Threading.Tasks;

namespace Model.D
{
    public class ProductD
    {
        OnlineShopDbContext db = null;
        public ProductD()
        {
            db = new OnlineShopDbContext();
        }
        public Product GetById(string name)
        {
            return db.Product.SingleOrDefault(x => x.Name == name);
        }
        public Product ViewDetail(long id)
        {
            return db.Product.Find(id);
        }
        public List<Product> ListAll()
        {
            return db.Product.Where(x => x.Status == true).ToList();
        }
        public long Insert(Product entity)
        {
            db.Product.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public List<string> ListName(string keyword)
        {
            return db.Product.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }
        public bool Update(Product entity)
        {
            try
            {
                var product = db.Product.Find(entity.ID);
                product.Name = entity.Name;
                product.Image = entity.Image;
                product.CategoryID = entity.CategoryID;
                product.Price = entity.Price;
                product.Code = entity.Code;
                product.CreatedDate = entity.CreatedDate;
                product.MetaTitle = entity.MetaTitle;
                product.ModifiedBy = entity.ModifiedBy;
                product.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Product;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString)).OrderByDescending(x => x.CreatedDate);
            }
            return model.OrderByDescending(X => X.CreatedDate).ToPagedList(page, pageSize);
        }
        public bool Delete(int id)
        {
            try
            {
                var product = db.Product.Find(id);
                db.Product.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ChangeStatus(long id)
        {
            var product = db.Product.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }
        public List<Product> ListNewProduct(int top)
        {
            return db.Product.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Product.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<Product> ListRelatedProducts(long productId)
        {
            var product = db.Product.Find(productId);
            return db.Product.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }
        public List<ProductViewModel> ListByCategoryId(long categoryID,ref int totalRecord,int pageIndex = 1,int pageSize = 2)
        {
            totalRecord = db.Product.Where(x => x.CategoryID == categoryID).Count();
            var model = from a in db.Product
                         join b in db.ProductCategory
                         on a.CategoryID equals b.ID
                         where a.CategoryID == categoryID
                         select new ProductViewModel()
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         };
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Product.Where(x => x.Name == keyword).Count();
            var model = (from a in db.Product
                         join b in db.ProductCategory
                         on a.CategoryID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

    }
}

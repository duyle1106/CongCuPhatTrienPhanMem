using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.D;


namespace Model.D
{
    public class ProductCategoryD
    {
        OnlineShopDbContext db = null;
        public ProductCategoryD()
        {
            db = new OnlineShopDbContext();
        }
        public ProductCategory GetById(string name)
        {
            return db.ProductCategory.SingleOrDefault(x => x.Name == name);
        }
        public long Insert(ProductCategory entity)
        {
            db.ProductCategory.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(ProductCategory entity)
        {
            try
            {
                var productcategory = db.ProductCategory.Find(entity.ID);
                productcategory.Name = entity.Name;
                productcategory.MetaTitle = entity.MetaTitle;
                productcategory.ParentID = entity.ParentID;
                productcategory.CreatedDate = entity.CreatedDate;
                productcategory.MetaTitle = entity.MetaTitle;
                productcategory.ModifiedBy = entity.ModifiedBy;
                productcategory.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<ProductCategory> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategory;
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
                var productcategory = db.ProductCategory.Find(id);
                db.ProductCategory.Remove(productcategory);
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
            var productcategory = db.ProductCategory.Find(id);
            productcategory.Status = !productcategory.Status;
            db.SaveChanges();
            return productcategory.Status;
        }

        public List<ProductCategory> ListAll()
        {
            return db.ProductCategory.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategory.Find(id);
        }
    }
}

using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.D
{
    public class CategoryD
    {
        OnlineShopDbContext db = null;
        public CategoryD()
        {
            db = new OnlineShopDbContext();
        }
        public List<Category> ListAll()
        {
            return db.Category.Where(x => x.Status == true).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategory.Find(id);
        }
        public long Insert(Category entity)
        {
            db.Category.Add(entity);
            db.SaveChanges();
            return entity.ID;

        }
        public bool Update(Category entity)
        {
            try
            {
                var category = db.Category.Find(entity.ID);
                category.Name = entity.Name;
                category.MetaTitle= entity.MetaTitle;
                category.ModifiedBy = entity.ModifiedBy;
                category.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Category> ListAllPaging(string searchString,int page,int pageSize)
        {
            IQueryable<Category> model = db.Category;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString)).OrderByDescending(x => x.CreatedDate);
            }
            return model.OrderByDescending(X=>X.CreatedDate).ToPagedList(page, pageSize);
        }
        public Category GetById(string name)
        {
            return db.Category.SingleOrDefault(x => x.Name == name);
        }
        public bool Delete(int id)
        {
            try
            {
                var category = db.Category.Find(id);
                db.Category.Remove(category);
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
            var cate = db.Category.Find(id);
            cate.Status = !cate.Status;
            db.SaveChanges();
            return cate.Status;
        }

      

    }
}

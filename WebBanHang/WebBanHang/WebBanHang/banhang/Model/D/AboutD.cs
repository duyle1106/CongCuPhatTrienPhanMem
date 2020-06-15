using Model.D;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.D
{
    public class AboutD
    {
        OnlineShopDbContext db = null;
        public AboutD()
        {
            db = new OnlineShopDbContext();
        }
        public List<About> ListAll()
        {
            return db.About.Where(x => x.Status == true).ToList();
        }
        public About ViewDetail(long id)
        {
            return db.About.Find(id);
        }
        public long Insert(About entity)
        {
            db.About.Add(entity);
            db.SaveChanges();
            return entity.ID;

        }
        public bool Update(About entity)
        {
            try
            {
                var about = db.About.Find(entity.ID);
                about.Name = entity.Name;
                about.Image = entity.Image;
                about.MetaTitle = entity.MetaTitle;
                about.ModifiedBy = entity.ModifiedBy;
                about.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<About> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<About> model = db.About;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString)).OrderByDescending(x => x.CreatedDate);
            }
            return model.OrderByDescending(X => X.CreatedDate).ToPagedList(page, pageSize);
        }
        public About GetById(string name)
        {
            return db.About.SingleOrDefault(x => x.Name == name);
        }
        public bool Delete(int id)
        {
            try
            {
                var about = db.About.Find(id);
                db.About.Remove(about);
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
            var about = db.About.Find(id);
            about.Status = !about.Status;
            db.SaveChanges();
            return about.Status;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.D
{
    public class SlideD
    {
        OnlineShopDbContext db = null;
        public SlideD()
        {
            db = new OnlineShopDbContext();
        }
        public List<Slide> ListAll()
        {
            return db.Slide.Where(x => x.Status == true).OrderBy(y => y.DisplayOrder).ToList();
        }
        public Slide ViewDetail(long id)
        {
            return db.Slide.Find(id);
        }
        public long Insert(Slide entity)
        {
            db.Slide.Add(entity);
            db.SaveChanges();
            return entity.ID;

        }
        public bool Update(Slide entity)
        {
            try
            {
                var slide = db.Slide.Find(entity.ID);
                slide.Image= entity.Image;
                slide.Link = entity.Link;
                slide.DisplayOrder = entity.DisplayOrder;
                slide.Description = entity.Description;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Slide> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Slide> model = db.Slide;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Link.Contains(searchString) || x.Link.Contains(searchString)).OrderByDescending(x => x.ModifiedDate);
                }
            return model.OrderByDescending(X => X.ModifiedDate).ToPagedList(page, pageSize);
        }
        public Slide GetById(string name)
        {
            return db.Slide.SingleOrDefault(x => x.Link == name);
        }
        public bool Delete(int id)
        {
            try
            {
                var slide = db.Slide.Find(id);
                db.Slide.Remove(slide);
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
            var slide= db.Slide.Find(id);
            slide.Status = !slide.Status;
            db.SaveChanges();
            return slide.Status;
        }
    }
}

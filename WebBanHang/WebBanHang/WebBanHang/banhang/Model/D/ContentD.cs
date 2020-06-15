using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Model.D
{
    public class ContentD
    {
        OnlineShopDbContext db = null;
        public ContentD()
        {
            db = new OnlineShopDbContext();
        }
        public List<Content> ListAll()
        {
            return db.Content.Where(x => x.Status == true).ToList();
        }
        public Content GetByID(long id)
        {
            return db.Content.Find(id);
        }
        public long Insert(Content entity)
        {
            db.Content.Add(entity);
            db.SaveChanges();
            return entity.ID;

        }
        public bool Update(Content entity)
        {
            try
            {
                var content = db.Content.Find(entity.ID);
                content.Name = entity.Name;
                content.CategoryID = entity.CategoryID;
                content.Image = entity.Image;
                content.Detail = entity.Detail;
                content.MetaTitle = entity.MetaTitle;
                content.ModifiedBy = entity.ModifiedBy;
                content.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Content;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.MetaTitle.Contains(searchString)).OrderByDescending(x => x.CreatedDate);
            }
            return model.OrderByDescending(X => X.CreatedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Content> ListAllByTag(string tag, int page, int pageSize)
        {
            var model = (from a in db.Content
                         join b in db.ContentTag
                         on a.ID equals b.ContentID
                         where b.TagID == tag
                         select new
                         {
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Image = a.Image,
                             Description = a.Description,
                             CreatedDate = a.CreatedDate,
                             CreatedBy = a.CreatedBy,
                             ID = a.ID

                         }).AsEnumerable().Select(x => new Content()
                         {
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Image = x.Image,
                             Description = x.Description,
                             CreatedDate = x.CreatedDate,
                             CreatedBy = x.CreatedBy,
                             ID = x.ID
                         });
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public Tag GetTag(string id)
        {
            return db.Tag.Find(id);
        }

        public Content ViewDetail(long id)
        {
            return db.Content.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var content = db.Content.Find(id);
                db.Content.Remove(content);
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
            var cont = db.Content.Find(id);
            cont.Status = !cont.Status;
            db.SaveChanges();
            return cont.Status;
        }



    }

}
      



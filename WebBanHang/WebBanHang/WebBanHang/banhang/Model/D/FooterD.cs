using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.D
{
    public class FooterD
    {
        OnlineShopDbContext db = null;
        public FooterD()
        {
            db = new OnlineShopDbContext();
        }
        public List<Footer> ListAll()
        {
            return db.Footer.Where(x => x.Status == true).ToList();
        }
        public string Insert(Footer entity)
        {
            db.Footer.Add(entity);
            db.SaveChanges();
            return entity.ID;

        }
        public bool Update(Footer entity)
        {
            try
            {
                var footer = db.Footer.Find(entity.ID);
                footer.Content= entity.Content;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Footer GetById(string id)
        {
            return db.Footer.SingleOrDefault(x => x.ID == id);
        }
        public Footer ViewDetail(string id)
        {
            return db.Footer.Find(id);
        }
        public bool Delete(string id)
        {
            try
            {
                var footer = db.Footer.Find(id);
                db.Footer.Remove(footer);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ChangeStatus(string id)
        {
            var footer= db.Footer.Find(id);
            footer.Status = !footer.Status;
            db.SaveChanges();
            return footer.Status;
        }
        public IEnumerable<Footer> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Footer> model = db.Footer;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.Contains(searchString) || x.Content.Contains(searchString)).OrderByDescending(x=>x.ID);
            }
            return model.OrderByDescending(X => X.ID).ToPagedList(page, pageSize);
        }
        public Footer GetFooter()
        {
            return db.Footer.SingleOrDefault(x => x.Status == true);
        }


    }
}

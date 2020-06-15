using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using Model.D;
using PagedList;

namespace Model.D
{
    public class MenuD
    {
        OnlineShopDbContext db = null;
        public MenuD()
        {
            db = new OnlineShopDbContext();
        }
        public List<Menu> ListByGroupId(int groupId)
        {
            return db.Menu.Where(x => x.TypeID == groupId && x.Status==true).OrderBy(x=>x.DisplayOrder).ToList();
        }
        public List<Menu> ListAll()
        {
            return db.Menu.Where(x => x.Status == true).ToList();
        }
        public Menu ViewDetail(long id)
        {
            return db.Menu.Find(id);
        }
        public long Insert(Menu entity)
        {
            db.Menu.Add(entity);
            db.SaveChanges();
            return entity.ID;

        }
        public bool Update(Menu entity)
        {
            try
            {
                var menu = db.Menu.Find(entity.ID);
                menu.Text = entity.Text;
                menu.Link = entity.Link;
                menu.DisplayOrder = entity.DisplayOrder;
                menu.TypeID = entity.TypeID;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Menu> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Menu> model = db.Menu;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Text.Contains(searchString) || x.Link.Contains(searchString)).OrderByDescending(x => x.TypeID);
            }
            return model.OrderByDescending(X => X.TypeID).ToPagedList(page, pageSize);
        }
        public Menu GetById(string name)
        {
            return db.Menu.SingleOrDefault(x => x.Text == name);
        }
        public bool Delete(int id)
        {
            try
            {
                var menu = db.Menu.Find(id);
                db.Menu.Remove(menu);
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
            var menu = db.Menu.Find(id);
            menu.Status = !menu.Status;
            db.SaveChanges();
            return menu.Status;
        }
    }
}

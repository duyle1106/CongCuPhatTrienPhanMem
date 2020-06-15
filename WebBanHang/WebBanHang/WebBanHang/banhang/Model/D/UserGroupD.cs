using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.D
{
    public class UserGroupD
    {
        OnlineShopDbContext db = null;
        public UserGroupD()
        {
            db = new OnlineShopDbContext();
        }
        public List<UserGroup> ListAll()
        {
            return db.UserGroup.Where(x => x.Status == true).ToList();
        }
        public UserGroup ViewDetail(long id)
        {
            return db.UserGroup.Find(id);
        }

    }
}

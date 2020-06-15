using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.D
{
    public class ContactD
    {
        OnlineShopDbContext db = null;
        public ContactD()
        {
            db = new OnlineShopDbContext();
        }
        public Contact GetActiveContact()
        {
            return db.Contact.Single(x => x.Status == true);
        }
        public int InsertFeedBack(Feedback fb)
        {
            db.Feedback.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}

using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.D
{
     public class OrderDetailD
    {
        OnlineShopDbContext db = null;
        public OrderDetailD()
        {
            db = new OnlineShopDbContext();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetail.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
   

    }
}

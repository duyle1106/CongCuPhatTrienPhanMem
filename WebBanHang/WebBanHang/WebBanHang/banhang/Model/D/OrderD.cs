using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.D
{
    public class OrderD
    {
        OnlineShopDbContext db = null;
        public OrderD()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Order order)
        {
            db.Order.Add(order);
            db.SaveChanges();
            return order.ID; 
        }
        public IEnumerable<Order> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Order;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipName.Contains(searchString) || x.ShipPet.Contains(searchString)).OrderByDescending(x => x.ID);
            }
            return model.OrderByDescending(X => X.ID).ToPagedList(page, pageSize);
        }

    }
}

using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.D
{

    public class FeedbackD
    {
        OnlineShopDbContext db = null;
        public FeedbackD()
        {
            db = new OnlineShopDbContext();
        }
        public IEnumerable<Feedback> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Feedback> model = db.Feedback;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Phone.Contains(searchString)).OrderByDescending(x => x.CreatedDate);
            }
            return model.OrderByDescending(X => X.CreatedDate).ToPagedList(page, pageSize);
        }
        public List<Feedback> ListAll()
        {
            return db.Feedback.Where(x => x.Status == false).ToList();
        }
        public Feedback ViewDetail(long id)
        {
            return db.Feedback.Find(id);
        }
        public Feedback GetById(string name)
        {
            return db.Feedback.SingleOrDefault(x => x.Name == name);
        }

    }
}

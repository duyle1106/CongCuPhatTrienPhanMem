using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
using CommonConstants;

namespace Model.D
{
    public class UserD
    {
        OnlineShopDbContext db = null;
        public UserD()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(User entity)
        {
            db.User.Add(entity);
            db.SaveChanges();
            return entity.ID;

        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.User.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.GroupID = entity.GroupID;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<User> ListAllPaging(string searchString, int page, int PageSize)
        {
            IQueryable<User> model = db.User;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString)).OrderByDescending(x=>x.CreatedDate);
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, PageSize);
        }
        public User GetById(string userName)
        {
            return db.User.SingleOrDefault(x => x.UserName == userName);
        }
        public User ViewDetail(long id)
        {
            return db.User.Find(id);
        }
        public List<string> GetListCreadential(string userName)
        {
            var user = db.User.Single(x => x.UserName == userName);
            var data = (from a in db.Credential
                        join b in db.UserGroup on a.UserGroupID equals b.ID
                        join c in db.Role on a.RoleID equals c.ID
                        where b.ID == user.GroupID
                        select new 
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID

                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();
        }

        public int login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.User.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupID == CommonConstants.CommonConstants.ADMIN_GROUP || result.GroupID == CommonConstants.CommonConstants.MOD_GROUP)
                    {
                        if (result.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == passWord)
                                return 1;
                            else
                                return -2;
                        }
                    }                
                else
                {
                    return -3;
                }
            }
                else
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == passWord)
                            return 1;
                        else
                            return -2;
                    }
                }
        
            }
        }
        public bool ChangeStatus(long id)
        {
            var user = db.User.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.User.Find(id);
                db.User.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
    
        }
        public bool CheckUserName(string userName)
        {
            return db.User.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckEmail(string email)
        {
            return db.User.Count(x => x.Email == email) > 0;
        }
        public List<User> ListAll()
        {
            return db.User.Where(x => x.Status == true).ToList();
        }
        


    }
}

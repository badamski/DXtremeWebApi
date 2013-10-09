using DXtremeWebApi.Models.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace DXtremeWebApi.Models.Repositories
{

    public interface IUserRepository
    {
        User GetUserById(int id);
        IQueryable<User> GetAll();
        IQueryable<User> GetUserByName(string searchstring);
        User Add(User user);
        User Update(User user);
        void Delete(int userId);

        IEnumerable<Notification> GetUserNotifications(int userId, string filter);
    }

    public class UserRepository : IUserRepository
    {
        private DXtremeWebApiContext _db { get; set; }

        public UserRepository(DXtremeWebApiContext db)
        {
            _db = db;
        }

        public User GetUserById(int id)
        {
            return _db.Users.Include("OwnedDocuments").FirstOrDefault(r => r.Id == id);
        }

        public IQueryable<User> GetUserByName(string searchstring)
        {
            return _db.Users.Where(u => u.UserName.Contains(searchstring));
        }

        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }

        public User Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User Update(User user)
        {
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();
            return user;
        }

        public void Delete(int userId)
        {
            var usertodel = GetUserById(userId);
            _db.Users.Remove(usertodel);
        }

        public IEnumerable<Notification> GetUserNotifications(int userId, string filter)
        {
            var dateNow = DateTime.Now.Date;

            switch (filter)
            {
                case "all":
                    return _db.Notifocations.Where(n => n.UserId == userId).OrderByDescending(n => n.Received);
                    break;

                case "today":
                    return _db.Notifocations.Where(n => n.UserId == userId && EntityFunctions.TruncateTime(n.Received.Date) == dateNow.Date).OrderBy(n => n.Received);
                    break;

                case "week":
                    return _db.Notifocations.Where(n => n.UserId == userId).OrderBy(n => n.Received).Take(20);
                    break;

                case "month":
                    return _db.Notifocations.Where(n => n.UserId == userId).OrderBy(n => n.Received).Take(20);
                    break;

                default:
                    return _db.Notifocations.Where(n => n.UserId == userId).OrderBy(n => n.Received).Take(20);
            }
        }
    }
}
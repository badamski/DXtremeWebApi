using DXtremeWebApi.Models;
using DXtremeWebApi.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DXtremeWebApi.Controllers
{
    public class UsersController : ApiController
    {
        private IUserRepository _userRepository { get; set; }

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public HttpResponseMessage Get(int Id)
        {
            var user = _userRepository.GetUserById(Id);

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        public IEnumerable<User> Get()
        {
            var allusers = _userRepository.GetAll();

            return allusers;
        }

        [HttpGet]
        public IEnumerable<User> GetByName(string searchstring)
        {
            var usersbyname = _userRepository.GetUserByName(searchstring);

            return usersbyname;
        }

        [HttpGet]
        public int UserAuth(string username, string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.UserName.ToLower() == username && u.Password.ToLower() == password);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return user.Id;
        }

        public HttpResponseMessage Post(User user)
        {
            var response = Request.CreateResponse(HttpStatusCode.Created, user);
            response.Headers.Location = new Uri(Request.RequestUri, string.Format("user/{0}", user.Id));
            _userRepository.Add(user);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            _userRepository.Delete(id);
            return response;
        }

        public void Put(int Id, User user)
        {
            _userRepository.Update(user);
        }

        [HttpGet]
        public IEnumerable<Notification> GetUserTimeline(int userId, string filter)
        {
            var notifications = _userRepository.GetUserNotifications(userId, filter);

            return notifications;
        }
    }
}

using DXtremeWebApi.Models;
using DXtremeWebApi.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DXtremeTest.Web.Controllers
{
    public class MessageController : ApiController
    {
        private IMessageRepository _messageRepository { get; set; } 
        private IUserRepository _userRepository { get; set; }

        public MessageController(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public HttpResponseMessage Options()
        {
            var response = Request.CreateResponse(HttpStatusCode.Accepted);
            response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE,HEAD,OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Authorization, X-Authorization");
            return response;
        }

        public HttpResponseMessage Get(int Id)
        {
            var message = _messageRepository.GetMessageById(Id);

            if (message == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, message);
        } 

        public HttpResponseMessage GetMessageDetails(int userId, int msgId)
        {
            var details = _messageRepository.GetMessageDetails(userId, msgId);

            if (details == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, details);
        }

        public IEnumerable<Message> Get()
        {
            var allmessages = _messageRepository.GetAll();

            return allmessages;
        }

        public IEnumerable<MessageHeader> GetUserMessages(int userId)
        {
            var recievedmsgs = _messageRepository.GetUserMessages(userId, "all");

            return recievedmsgs;
        }

        public IEnumerable<MessageHeader> GetUserInboxMessages(int userId)
        {
            var recievedmsgs = _messageRepository.GetUserMessages(userId, "inbox");

            return recievedmsgs;
        }

        public IEnumerable<MessageHeader> GetUserSentMessages(int userId)
        {
            var recievedmsgs = _messageRepository.GetUserMessages(userId, "sent");

            return recievedmsgs;
        }

        public HttpResponseMessage Post(MessageWithHeader msg)
        {
            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, string.Format("message/{0}", msg.messageTitle));
            _messageRepository.SendMessage ( new Message()
                { 
                    MessageTitle = msg.messageTitle, 
                    MessageContent = msg.messageContent }, msg.fromId, msg.toId);

            return response;
        }

        public HttpResponseMessage Delete(int Id)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            _messageRepository.Delete(Id);
            return response;
        }

        public void Put(int Id, Message message)
        {
            _messageRepository.Update(message);
        }
    }
}


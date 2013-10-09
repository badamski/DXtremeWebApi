using DXtremeWebApi.Models.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXtremeWebApi.Models.Repositories
{
    public interface IMessageRepository
    {
        Message GetMessageById(int id);
        IQueryable<Message> GetAll();
        Message Add(Message message);
        Message Update(Message message);
        void Delete(int messageId);
        IEnumerable<MessageHeader> GetUserMessages(int userId, string filter);
        MessageHeader GetMessageDetails(int userId, int msgId);

        MessageHeader SendMessage(Message message, int fromId, int toId);
    }

    public class MessageRepository : IMessageRepository
    {
        private DXtremeWebApiContext _db { get; set; }

        public MessageRepository(DXtremeWebApiContext db)
        {
            _db = db;
        }

        public Message GetMessageById(int id)
        {
            return _db.Messages.FirstOrDefault(r => r.Id == id);
        }

        public IQueryable<Message> GetAll()
        {
            return _db.Messages;
        }

        public Message Add(Message message)
        {
            _db.Messages.Add(message);
            _db.SaveChanges();
            return message;
        }

        public Message Update(Message message)
        {
            _db.Entry(message).State = EntityState.Modified;
            _db.SaveChanges();
            return message;
        }

        public void Delete(int headerId)
        {
            var header = _db.MessageHeaders.Include("Message").Include("To").Include("From").FirstOrDefault(h=>h.Id == headerId);

            _db.MessageHeaders.Remove(header);

           // var msgtodel = GetMessageById(headerId);
           // _db.Messages.Remove(msgtodel);
        }

        public MessageHeader SendMessage(Message message, int fromId, int toId)
        {
            var userfrom = _db.Users.FirstOrDefault(u => u.Id == fromId);
            var userto = _db.Users.FirstOrDefault(u => u.Id == toId);

            var header = new MessageHeader()
            {
                From = userfrom,
                To = userto,
                Sent = DateTime.Now,
                Message = message
            };

            _db.MessageHeaders.Add(header);          
            _db.SaveChanges();

            AddEventNotification(message, userto, userfrom);

            return header;
        }

        public MessageHeader GetMessageDetails(int userId,int msgId)
        {
            var header = _db.MessageHeaders.Include("Message").Include("To").Include("From").ToList();

            var message = header.FirstOrDefault(h => h.To.Id == userId && h.Message.Id == msgId);

            //var result = new MessageWithHeader()
            //{
            //    fromId = message.From.Id,
            //    messageTitle = message.Message.MessageTitle,
            //    messageContent = message.Message.MessageContent
            //};

            return message; 
        }

        private void AddEventNotification(Message message, User userToNotify, User userFrom)
        {
            var date = DateTime.Now;

            var notification = new Notification()
            {
                Received = date,
                NotificationText = String.Format("{0} - MESSAGE from user {1}", date.ToShortTimeString(), userFrom.UserName),
                RelatedMessage = message
            };

            userToNotify.Notifications.Add(notification);
            _db.SaveChanges();
        }

        public IEnumerable<MessageHeader> GetUserMessages(int userId, string filter)
        {
            var header = _db.MessageHeaders.Include("Message").Include("To").Include("From").ToList();

            switch (filter)
            {
                case "inbox":
                        return header.Where(h => h.To.Id == userId).OrderByDescending(h=>h.Sent);
                    break;

                case "sent":
                    return header.Where(h => h.From.Id == userId).OrderByDescending(h => h.Sent);
                    break;

                default:
                    return header.OrderByDescending(h => h.Sent);
                    break;
            }

            //var recievedmsgs = header.Where(h => h.To.Id == userId);
            //var sentmsgs = header.Where(h => h.From.Id == userId);

           // return sentmsgs;
        }
    }
}
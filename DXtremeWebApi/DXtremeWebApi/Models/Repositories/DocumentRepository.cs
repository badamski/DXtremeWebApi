using DXtremeWebApi.Models.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXtremeWebApi.Models.Repositories
{
    public interface IDocumentRepository
    {
        Document GetDocById(int id);
        IQueryable<Document> GetAll();
        Document Add(Document doc);
        Document Update(Document doc);
        void Delete(int docId);

        void ShareDocument(int docId, int[] userIds);
        IEnumerable<Document> GetUserDocuments(int userId);
    }

    public class DocumentRepository : IDocumentRepository
    {

        private DXtremeWebApiContext _db { get; set; }

        public DocumentRepository(DXtremeWebApiContext db)
        {
            _db = db;
        }

        public Document GetDocById(int id)
        {
            return _db.Documents.FirstOrDefault(r => r.Id == id);
        }

        public IQueryable<Document> GetAll()
        {
            return _db.Documents;
        }

        public Document Add(Document doc)
        {
            _db.Documents.Add(doc);
            _db.SaveChanges();
            return doc;
        }

        public Document Update(Document doc)
        {
            _db.Entry(doc).State = EntityState.Modified;
            _db.SaveChanges();
            return doc;
        }

        public void Delete(int docId)
        {
            var doctodel = GetDocById(docId);
            _db.Documents.Remove(doctodel);
        }

        public IEnumerable<Document> GetUserDocuments(int userId)
        {
            var user = _db.Users.Include("OwnedDocuments").FirstOrDefault(u => u.Id == userId);

            return user.OwnedDocuments;
        }

        public void ShareDocument(int docId, int[] userIds)
        {
            for (int i = 0; i < userIds.Length; i++)
            {
                if (userIds[i] != null)
                {
                    var document = _db.Documents.Include("AllowedUsers").FirstOrDefault(d => d.Id == docId);
                    var user = _db.Users.FirstOrDefault(u => u.Id == userIds[i]);

                    document.AllowedUsers.Add(user);
                }

                _db.SaveChanges();
            }
        }

        private void AddEventNotification(Document doc, User userToNotify, User userFrom)
        {
            var date = DateTime.Now;

            var notification = new Notification()
            {
                Received = date,
                NotificationText = String.Format("{0} - Document recieved from user {1}", date.ToShortTimeString(), userFrom.UserName),
                RelatedDocument = doc
            };

            userToNotify.Notifications.Add(notification);

            _db.SaveChanges();
        }
    }
}

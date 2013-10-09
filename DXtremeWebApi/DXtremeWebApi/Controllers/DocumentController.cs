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
    public class DocumentController : ApiController
    {
        private IDocumentRepository _documentRepository { get; set; }

        public DocumentController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
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
            var doc = _documentRepository.GetDocById(Id);

            if (doc == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, doc);
        }

        public IEnumerable<Document> Get()
        {
            var alldocs = _documentRepository.GetAll();

            return alldocs;
        }

        public HttpResponseMessage Post(Document doc)
        {
            var response = Request.CreateResponse(HttpStatusCode.Created, doc);
            response.Headers.Location = new Uri(Request.RequestUri, string.Format("document/{0}", doc.Id));
            _documentRepository.Add(doc);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            _documentRepository.Delete(id);
            return response;
        }

        public void Put(int Id, Document doc)
        {
            _documentRepository.Update(doc);
        }

        public IEnumerable<Document> GetUserDocuments(int userId)
        {
            var userDocuments = _documentRepository.GetUserDocuments(userId);

            return userDocuments;
        }

        [HttpPost]
        public HttpResponseMessage ShareDocument(int docId, int userId1, int userId2 ,int userId3)
        {
            var response = Request.CreateResponse(HttpStatusCode.Created, docId);
            response.Headers.Location = new Uri(Request.RequestUri, string.Format("document/{0}", docId));

            int[] userIds = new int[3]{userId1,userId2,userId3};

            _documentRepository.ShareDocument(docId,userIds) ;

            return response;
        }
    }
}
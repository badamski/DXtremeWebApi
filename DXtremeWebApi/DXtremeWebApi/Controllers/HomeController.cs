using DXtremeWebApi.Models;
using DXtremeWebApi.Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXtremeWebApi.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository _userRepository { get; set; }
        private IDocumentRepository _documentRepository { get; set; }
        public IMessageRepository _messageRepository { get; set; }

        public HomeController(IUserRepository userRepository, IDocumentRepository documentRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _documentRepository = documentRepository;
            _messageRepository = messageRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UsersList()
        {
            var allusers = _userRepository.GetAll();

            return View(allusers);
        }

        public ActionResult DocsList()
        {
            var alldocs = _documentRepository.GetAll();

            return View(alldocs);
        }

        public ActionResult EditUser(int Id)
        {
            var user = _userRepository.GetUserById(Id);

            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            var files = Directory.GetFiles(Server.MapPath("./../../Temp//"));
            var usertoedit = _userRepository.GetUserById(user.Id);

            foreach (var item in files)
            {
                var file = new FileInfo(item);
                file.MoveTo(Server.MapPath("./../../DocumentsData//" + file.Name));

                usertoedit.OwnedDocuments.Add(new Document()
                {
                    FileName = file.Name,
                    FilePath = @"./DocumentsData/" + file.Name,
                    FileSize = file.Length.ToString() + "KB",
                    Created = DateTime.Now
                });
            }

            _userRepository.Update(usertoedit);

            return RedirectToAction("DocsList");
        }

        public ActionResult Populate()
        {
            //User initialization
            #region
            //_userRepository.Add(new User
            //{
            //    UserName = "Adam",
            //    Password = "passadam",
            //    Created = DateTime.Now,
            //});

            var adam = new User()
            {
                UserName = "Adam",
                Password = "passadam",
                Created = DateTime.Now,
            };
            adam.OwnedDocuments.Add(new Document
            {
                FileName = "pdf1.pdf",
                FilePath = @"./DocumentsData/pdf1.pdf",
                FileSize = "24 kB",
                Created = DateTime.Now
            });
            adam.OwnedDocuments.Add(new Document
            {
                FileName = "pdf2.pdf",
                FilePath = @"./DocumentsData/pdf2.pdf",
                FileSize = "24 kB",
                Created = DateTime.Now
            });

            _userRepository.Add(adam);

            _userRepository.Add(new User
            {
                UserName = "John",
                Password = "passjohn",
                Created = DateTime.Now
            });
            _userRepository.Add(new User
            {
                UserName = "Michael",
                Password = "passmichael",
                Created = DateTime.Now
            });
            _userRepository.Add(new User
            {
                UserName = "Scott",
                Password = "passscot",
                Created = DateTime.Now
            });
            _userRepository.Add(new User
            {
                UserName = "Andy",
                Password = "passandy",
                Created = DateTime.Now
            });
            _userRepository.Add(new User
            {
                UserName = "Dave",
                Password = "passdave",
                Created = DateTime.Now
            });
            #endregion
            // Documents Initialization
            #region
            //_documentRepository.Add(new Document
            //{
            //    FileName = "pdf1.pdf",
            //    FilePath = @"./DocumentsData/pdf1.pdf",
            //    FileSize = "24 kB",
            //    Created = DateTime.Now
            //});
            //_documentRepository.Add(new Document
            //{
            //    FileName = "pdf2.pdf",
            //    FilePath = @"./DocumentsData/pdf2.pdf",
            //    FileSize = "145 kB",
            //    Created = DateTime.Now
            //});
            //_documentRepository.Add(new Document
            //{
            //    FileName = "Przetarg.pdf",
            //    FilePath = "",
            //    FileSize = "3456 kB",
            //    Created = DateTime.Now
            //});
            //_documentRepository.Add(new Document
            //{
            //    FileName = "Faktura/06/02/2013.pdf",
            //    FilePath = "",
            //    FileSize = "145 kB",
            //    Created = DateTime.Now
            //});
            //_documentRepository.Add(new Document
            //{
            //    FileName = "Raport_zamowienia.pdf",
            //    FilePath = "",
            //    FileSize = "24 kB",
            //    Created = DateTime.Now
            //});
            //_documentRepository.Add(new Document
            //{
            //    FileName = "Faktura/30/07/2008.pdf",
            //    FilePath = "",
            //    FileSize = "3456 kB",
            //    Created = DateTime.Now
            //});
            #endregion

            return View("Index");
        }

        [HttpPost]
        public void FileUpload(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("./../Temp//" + file.FileName));
        }
    }
}

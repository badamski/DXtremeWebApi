using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DXtremeWebApi.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime Created { get; set; }

        public List<Notification> Notifications { get; set; }

        public List<Document> OwnedDocuments { get; set; }
        public List<Document> DocumentsToAccept { get; set; }

        public User()
        {
            Notifications = new List<Notification>();
            OwnedDocuments = new List<Document>();
            DocumentsToAccept = new List<Document>();
        }
    }
}
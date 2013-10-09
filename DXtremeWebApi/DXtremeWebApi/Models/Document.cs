using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DXtremeWebApi.Models
{
    public class Document
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        public string FilePath { get; set; }

        [Required]
        public string FileSize { get; set; }
        public DateTime Created { get; set; }

        public List<User> AllowedUsers { get; set; }

        public Document()
        {
            AllowedUsers = new List<User>();
        }
    }
}
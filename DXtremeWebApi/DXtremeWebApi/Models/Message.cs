using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DXtremeWebApi.Models
{
    public class Message
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string MessageTitle { get; set; }
        [Required]
        public string MessageContent { get; set; }

        //public Document AttachedDocument { get; set; }
    }
}
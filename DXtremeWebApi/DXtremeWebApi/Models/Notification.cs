using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DXtremeWebApi.Models
{
    public class Notification
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string NotificationText { get; set; }
        [Required]
        public DateTime Received { get; set; }

        public int UserId { get; set; }

        public int RelatedMessageId { get; set; }

        public Document RelatedDocument { get; set; }
        public Message RelatedMessage { get; set; }
    }
}
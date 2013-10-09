using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DXtremeWebApi.Models
{
    public class MessageHeader
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public virtual User From { get; set; }
        public virtual User To { get; set; }

        public bool IsRead { get; set; }

        [Required]
        public DateTime Sent { get; set; }
        [Required]
        public virtual Message Message { get; set; }
    }
}
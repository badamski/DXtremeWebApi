using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXtremeWebApi.Models
{
    public class MessageWithHeader
    {
        public int fromId { get; set; }
        public int toId { get; set; }

        public string messageTitle { get; set; }
        public string messageContent { get; set; }
    }
}
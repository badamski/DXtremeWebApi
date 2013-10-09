using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace DXtremeWebApi.Models.Context
{
    public class DXtremeWebApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifocations { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageHeader> MessageHeaders { get; set; }
        public DbSet<EdmMetadata> EdmMetadata { get; set; }


        public DXtremeWebApiContext()
            : base("TestAppConnection")
        {
           // Database.SetInitializer(new AplikacjaTestowaContextInitializer());
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = true;
        }
    }
}
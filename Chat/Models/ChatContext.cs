using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Chat.Models
{ 
    public class ChatContext : DbContext
    {
        public ChatContext()
            : base("ChatContext")
        {
    
        }
        public DbSet<User> Users { get; set; }
        public DbSet<TextMessage> TextMessages { get; set; }

         protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Chat.Models
{ //Создаем контекст подключения к базе 
    public class ChatContext : DbContext
    {
        public ChatContext()
            : base("ChatContext")
        {
            //Database.SetInitializer<ChatContext>(new DropCreateDatabaseAlways<ChatContext>());
        }
        //получаем список объектов базы данных
        public DbSet<User> Users { get; set; }
        public DbSet<TextMessage> TextMessages { get; set; }

         protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
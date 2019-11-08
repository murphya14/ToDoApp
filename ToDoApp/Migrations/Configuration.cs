namespace ToDoApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity; //input
    using Microsoft.AspNet.Identity.EntityFramework; //input
    using ToDoApp.Models; //input


    internal sealed class Configuration : DbMigrationsConfiguration<ToDoApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ToDoApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            AddUsers(context); //created
        }

        void AddUsers(ToDoApp.Models.ApplicationDbContext context) //Created
        {
            var user = new ApplicationUser { UserName = "user1@email.com" };
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            um.Create(user, "password");
        }
    }
}

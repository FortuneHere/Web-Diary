using System.Data.Entity.Migrations;
using WebDiary.DB.Models;

namespace WebDiary.DB.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration() =>
            AutomaticMigrationsEnabled = false;

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use teh DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FulNlame,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Users.AddOrUpdate(new User
            {
                Id = 1,
                Login = "admin@aa.ru",
                Password = "123",
                FirstName = "Админ",
                LastName = "Админский",
                MiddleName = "Админович",
                Teacher = new Teacher
                {
                    IsAdministrator = true
                }
            });
        }
    }
}
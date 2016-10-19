namespace S00156762Rad2016Mvc1.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<S00156762Rad2016Mvc1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(S00156762Rad2016Mvc1.Models.ApplicationDbContext context)
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

            // Seed Roles

            var manager =
                new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

            if (context.Roles.FirstOrDefault(r => r.Name == "Admin") == null)
            {
                context.Roles.Add(new IdentityRole { Name = "Admin" });
            }

            context.Roles.AddOrUpdate(r => r.Name, 
                new IdentityRole { Name = "ClubAmin" });

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Member"});

            context.SaveChanges();
            //Seed Users
            PasswordHasher p = new PasswordHasher();

            context.Users.AddOrUpdate(u => u.StudentID,
                new ApplicationUser {
                    StudentID = "S00000001",
                    Email = "S00000001@mail.itsligo.ie",
                    PasswordHash = p.HashPassword("Sxxxxxxx1$1"),
                    SignupDate = DateTime.Now,
                    UserName = "Bill Gates",
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser {
                    StudentID = "S00000002",
                    Email = "S00000002@mail.itsligo.ie",
                    PasswordHash = p.HashPassword("Sxxxxxxx2$1"),
                    SignupDate = DateTime.Now,
                    UserName = "Mark Zuckerberg",
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    StudentID = "S00000003",
                    Email = "S00000003@mail.itsligo.ie",
                    PasswordHash = p.HashPassword("Sxxxxxxx3$1"),
                    SignupDate = DateTime.Now,
                    UserName = "Paul Little",
                    SecurityStamp = Guid.NewGuid().ToString()
                });
            context.SaveChanges();
            
            manager.Create(new ApplicationUser
            {
                StudentID = "S00000004",
                Email = "S00000004@mail.itsligo.ie",                
                SignupDate = DateTime.Now,
                UserName = "TomasLittle",
                SecurityStamp = Guid.NewGuid().ToString()
            }, "Sxxxxxxx4$1");

            manager.Create(new ApplicationUser
            {
                StudentID = "S00156762",
                Email = "S00156762@mail.itsligo.ie",
                SignupDate = DateTime.Now,
                UserName = "AislingLittle",
                SecurityStamp = Guid.NewGuid().ToString()
            }, "aAlittle$1");
            context.SaveChanges();

            // setup Tomas Little as System Admin Role
            IdentityRole adminRole = roleManager.FindByName("Admin");
            if (adminRole!=null)
            {
                ApplicationUser adminUser = manager.FindByName("TomasLittle");
                if (adminUser != null && adminUser.Roles.Count < 1)
                {
                    adminUser.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id, UserId = adminUser.Id});
                }
            }

            // setup Paulo Little as Club Admin Role
            IdentityRole clubAdminRole = roleManager.FindByName("ClubAdmin");
            if (clubAdminRole != null)
            {
                ApplicationUser adminUser = manager.FindByName("Paul Little");
                if (adminUser != null && adminUser.Roles.Count < 1)
                {
                    adminUser.Roles.Add(new IdentityUserRole { RoleId = clubAdminRole.Id, UserId = clubAdminRole.Id });
                }
            }

            // setup Aisling Little as System Member Role
            IdentityRole memberRole = roleManager.FindByName("Member");
            if (memberRole != null)
            {
                ApplicationUser adminUser = manager.FindByName("AislingLittle");
                if (adminUser != null && adminUser.Roles.Count < 1)
                {
                    adminUser.Roles.Add(new IdentityUserRole { RoleId = memberRole.Id, UserId = memberRole.Id });
                }
            }
            context.SaveChanges();
        }        
    }
}

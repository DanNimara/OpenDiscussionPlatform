using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OpenDiscussionPlatform.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public IEnumerable<SelectListItem> AllRoles { get; set; }

        [StringLength(100, ErrorMessage = "Prenumele nu poate avea mai mult de 100 de caractere!")]
        public string FirstName { get; set; }
        [StringLength(100, ErrorMessage = "Numele sectiune nu poate avea mai mult de 100 de caractere!")]
        public string LastName { get; set; }
        [StringLength(500, ErrorMessage = "Aceasta sectiune nu poate avea mai mult de 500 de caractere!")]
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }
        public DateTime RegisterDate { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, OpenDiscussionPlatform.Migrations.Configuration>("DefaultConnection"));
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Reply> Replies { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}

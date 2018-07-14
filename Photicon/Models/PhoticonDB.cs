using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Dynamic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Photicon.Models
{
    // You can add profile data for the user by adding more properties to your Users class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class PhoticonDB : IdentityDbContext<Users>
    {
        public PhoticonDB() : base("PhoticonDBConnectionString") { }

        //tabelas na Base de Dados
        public virtual DbSet<Pictures> Pictures { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static PhoticonDB Create()
        {
            return new PhoticonDB();
        }
    }
}
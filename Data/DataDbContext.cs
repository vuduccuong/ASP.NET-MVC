using Microsoft.AspNet.Identity.EntityFramework;
using MODEL;
using System.Data.Entity;

namespace Data
{
    public class DataDbContext : IdentityDbContext<ApplicationUser>
    {
        public DataDbContext() : base("DataConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Employee> Employees { set; get; }

        //Phương thức tạo mới chính nó
        public static DataDbContext Create()
        {
            return new DataDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
        }
        private void FixEfProviderServicesProblem()
        {
            
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
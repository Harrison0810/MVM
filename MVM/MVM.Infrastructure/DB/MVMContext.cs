using Microsoft.EntityFrameworkCore;
using MVM.Infrastructure.Entities;

namespace MVM.Infrastructure.DB
{
    public class MVMContext : DbContext
    {
        public MVMContext(DbContextOptions<MVMContext> options) : base(options)
        {
            Database.EnsureCreated();
            Database.SetCommandTimeout(180);
        }

        #region Entities Definition

        public DbSet<RolesEntity> Roles { get; set; }
        public DbSet<CorrespondencesEntity> Correspondences { get; set; }
        public DbSet<CorrespondenceTypesEntity> CorrespondenceTypes { get; set; }
        public DbSet<LogEntity> Log { get; set; }
        public DbSet<UsersEntity> Users { get; set; }

        #endregion

        #region Model Creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolesEntity>().ToTable("Roles");
            modelBuilder.Entity<CorrespondencesEntity>().ToTable("Correspondences");
            modelBuilder.Entity<CorrespondenceTypesEntity>().ToTable("CorrespondenceTypes");
            modelBuilder.Entity<LogEntity>().ToTable("Log");
            modelBuilder.Entity<UsersEntity>().ToTable("Users");
        }

        #endregion
    }
}

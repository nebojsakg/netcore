using Domain.Model;
using Innofactor.EfCoreJsonValueConverter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Domain
{
    public class DatabaseContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.StateChanged += (object sender, EntityStateChangedEventArgs e) =>
            {
                DateTime currentTime = DateTime.Now;

                if (e.Entry.Entity is BaseModel)
                {
                    BaseModel entityBase = e.Entry.Entity as BaseModel;

                    if (e.Entry.State == EntityState.Modified)
                    {
                        entityBase.UpdateAt = currentTime;
                        entityBase.UpdatedById = CurrentUser.Id;
                    }
                }
            };

            ChangeTracker.Tracked += (object sender, EntityTrackedEventArgs e) =>
            {
                DateTime currentTime = DateTime.Now;

                if (e.Entry.Entity is BaseModel)
                {
                    BaseModel entityBase = e.Entry.Entity as BaseModel;

                    if (e.Entry.State == EntityState.Added)
                    {
                        entityBase.CreatedById = CurrentUser.Id;
                        entityBase.UpdateAt = currentTime;
                        entityBase.UpdatedById = CurrentUser.Id;
                    }
                }
            };
        }

        public User CurrentUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.AddJsonFields();
        }
    }
}

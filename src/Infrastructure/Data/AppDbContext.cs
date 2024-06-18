using Core.Entities;
using Core.Entities.Users;
using Infrastructure.Data.Config;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        // private readonly DbContextOptions _options;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // _options = options;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<ParentStudent> ParentStudent { get; set; }
        public DbSet<Busdriver> Busdrivers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<QrCode> QrCodes { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripStudent> TripStudents { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<SubscriptPlan> SubscriptPlans { get; set; }
        public DbSet<SubscriptBenefits> SubscriptBenefits { get; set; }
        public DbSet<SubscriptPlanBenefit> SubscriptPlanBenefits { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSubscript> UserSubscripts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            var entityTypes = builder.Model.GetEntityTypes();
            entityTypes.ToList().ForEach(entityType =>
            {
                if (typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            });

            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<Parent>()
                    .HasMany(e => e.Students)
                    .WithMany(e => e.Parents)
                    .UsingEntity<ParentStudent>();

            builder.Entity<TripStudent>()
                .HasKey(ts => new { ts.TripId, ts.StudentId }); // Composite key

            builder.Entity<TripStudent>()
                .HasOne(ts => ts.Trip)
                .WithMany(t => t.TripStudents)
                .HasForeignKey(ts => ts.TripId);

            builder.Entity<TripStudent>()
                .HasOne(ts => ts.Student)
                .WithMany(s => s.TripStudents)
                .HasForeignKey(ts => ts.StudentId);

            // builder.Entity<SubscriptPlanBenefit>()
            //     .HasKey(pc => new { pc.SubscriptPlanId, pc.SubscriptBenefitsId });

            // builder.Entity<SubscriptPlanBenefit>()
            //     .HasOne(pc => pc.SubscriptPlan)
            //     .WithMany(p => p.SubscriptPlanBenefits)
            //     .HasForeignKey(pc => pc.SubscriptPlanId);

            // builder.Entity<SubscriptPlanBenefit>()
            //     .HasOne(pc => pc.SubscriptBenefits)
            //     .WithMany(c => c.SubscriptPlanBenefits)
            //     .HasForeignKey(pc => pc.SubscriptBenefitsId);

            // builder.Entity<SubscriptPlan>()
            //     .HasMany(e => e.PersonaId)
            //     .WithOne(e => e.SubscriptPlan)
            //     .HasForeignKey(e => e.SubscriptPlanId)
            //     .IsRequired(false);

        }

        public async Task<bool> TrySaveChangesAsync()
        {
            try            
            {
                await SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }

}

using Gharshare.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Gharshare.DataAccessLayer
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PersonListing> PersonListings { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<ListingEmployment> ListingEmployments { get; set; }
        public DbSet<Lifestyle> Lifestyles { get; set; }
        public DbSet<ListingLifestyle> ListingLifestyles { get; set; }
        public DbSet<ListingPropertyType> ListingPropertyTypes { get; set; }
        public DbSet<Furnishing> Furnishings { get; set; }
        public DbSet<RoomFurnishing> RoomFurnishings { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<EmailTemplateType> EmailTemplateTypes { get; set; }
        public DbSet<Shortlist> Shortlist { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<UserNotificationType> UserNotificationTypes { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanService> PlanServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<UserPlan> UserPlans { get; set; }
        public DbSet<UserPayment> UserPayments { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Sitesetting> Sitesettings { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Suburb> Suburbs { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<StaticContent> StaticContent { get; set; }
        public DbSet<Toolkit> Toolkit { get; set; }
        public DbSet<PropertyInspection> PropertyInspections { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<UserBankAccount> UserBankAccounts { get; set; }
        public DbSet<PropertyFeature> PropertyFeatures { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Accepting> Acceptings { get; set; }
        public DbSet<PropertyAccepting> PropertyAcceptings { get; set; }
        public DbSet<PropertiesViewed> PropertiesViewed { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<DemoRequest> DemoRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("dbo");
            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
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
            // Define the relationship between ApplicationUser and UserNotification
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.notificationSettings)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserNotification>()
           .HasOne(un => un.CreatedbyUser)
           .WithMany()
           .HasForeignKey(un => un.CreatedbyUserID)
           .OnDelete(DeleteBehavior.NoAction);


            // Define the relationship between ApplicationUser and Errorlog
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.errolog)
                .WithOne(n => n.LoggedInUser)
                .HasForeignKey(n => n.LoggedInUserId);

            builder.Entity<Room>() //cascade delete enquries when room deleted
           .HasMany(p => p.enquiries)
           .WithOne(c => c.Room)
           .OnDelete(DeleteBehavior.Cascade);

            // builder.Entity<Property>() //cascade delete enquries when property deleted
            //.HasMany(p => p.Enquiries)
            //.WithOne(c => c.Property)
            //.OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Shortlist>() //cascade delete shortlist listings when personlisting deleted
                .HasOne(s => s.PersonListing)
                .WithMany(pl => pl.Shortlists)
                .HasForeignKey(s => s.PersonlistingId)
                .OnDelete(DeleteBehavior.Cascade);

            #region index on UId field
            builder.Entity<Accepting>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<City>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<EmailTemplate>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<EmailTemplateType>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Employment>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Enquiry>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<ErrorLog>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Feature>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Furnishing>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Lifestyle>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<ListingEmployment>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<ListingLifestyle>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<ListingPropertyType>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Meeting>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Message>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Person>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<PersonListing>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<PersonListingViewed>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Plan>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<PlanService>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<PropertiesViewed>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Property>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<PropertyAccepting>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<PropertyFeature>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<PropertyImage>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<PropertyInspection>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<PropertyType>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Room>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<RoomFurnishing>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Service>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Shortlist>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Sitesetting>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<State>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<StaticContent>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Suburb>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<Testimonial>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<UserNotification>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<UserNotificationType>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<UserPayment>().HasIndex(e => e.UId).IsUnique();
            builder.Entity<UserPlan>().HasIndex(e => e.UId).IsUnique();
            #endregion
        }
    }
    public class DataContextFactory : IDesignTimeDbContextFactory<ApplicationDBContext>
    {
        public ApplicationDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            var builder = WebApplication.CreateBuilder(args);
            optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            return new ApplicationDBContext(optionsBuilder.Options);
        }
    }

}

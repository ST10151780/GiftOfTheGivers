using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<VolunteerProfile> VolunteerProfiles { get; set; }

        // NEW: Add Assignments
        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure one-to-many: VolunteerProfile -> Assignments
            builder.Entity<Assignment>()
                .HasOne(a => a.Volunteer)
                .WithMany(v => v.Assignments)
                .HasForeignKey(a => a.VolunteerProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}

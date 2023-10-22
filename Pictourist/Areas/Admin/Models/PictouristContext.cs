using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pictourist.Areas.Admin.Models;

namespace Pictourist.Admin.Models
{
    public class PictouristContext : IdentityDbContext<User>
    {
        public DbSet<Friend> Friends { get; set; }

        public PictouristContext(DbContextOptions<PictouristContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<User>()
        //    .Metadata.FindNavigation(nameof(User.UserPhotos))
        //    .SetPropertyAccessMode(PropertyAccessMode.Field);
        //}
    }
}

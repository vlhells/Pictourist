using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pictourist.Models
{
	public class PictouristContext: IdentityDbContext<User>
	{
		//public DbSet<User> Users { get; set; }

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

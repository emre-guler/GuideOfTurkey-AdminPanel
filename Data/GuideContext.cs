using GuideOfTurkey_AdminPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace GuideOfTurkey_AdminPanel.Data
{
    public class GuideContext : DbContext 
    {
        public GuideContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<City>().Property(x => x.deleteState).HasDefaultValue(0);
            
            builder.Entity<CityGallery>().Property(x => x.deleteState).HasDefaultValue(0);
            
            builder.Entity<Comment>().Property(x => x.deleteState).HasDefaultValue(0);
            builder.Entity<Comment>().Property(x => x.Date).HasDefaultValueSql("getDate()");

            builder.Entity<Country>().Property(x => x.deleteState).HasDefaultValue(0);
            
            builder.Entity<CountryGallery>().Property(x => x.deleteState).HasDefaultValue(0);

            builder.Entity<District>().Property(x => x.deleteState).HasDefaultValue(0);

            builder.Entity<DistrictGallery>().Property(x => x.deleteState).HasDefaultValue(0);

            builder.Entity<Photo>().Property(x => x.sliderState).HasDefaultValue(0);
            builder.Entity<Photo>().Property(x => x.deleteState).HasDefaultValue(0);

            builder.Entity<Favorite>().Property(x => x.deleteState).HasDefaultValue(0);

            builder.Entity<Place>().Property(x => x.deleteState).HasDefaultValue(0);

            builder.Entity<PlaceType>().Property(x => x.deleteState).HasDefaultValue(0);

            builder.Entity<PlaceType>().Property(x => x.deleteState).HasDefaultValue(0);

            builder.Entity<UserAccount>().Property(x => x.deleteState).HasDefaultValue(0);
            builder.Entity<UserAccount>().Property(x => x.createdAt).HasDefaultValueSql("getDate()");
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<CityGallery> CityGalleries { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryGallery> CountryGalleries { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<DistrictGallery> DistrictGalleries { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Place> Places { get; set;}  
        public DbSet<PlaceGallery> PlaceGalleries { get; set; }
        public DbSet<PlaceType> Types { get; set; }
        public DbSet<UserAccount> userAccounts { get; set; }
    }
}
using Ecommerce.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Data.Context
{
    public class DipoleEcommerceContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Delivery> Deliverys { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<WishListItem> WishlistItems { get; set; }
        public DipoleEcommerceContext(DbContextOptions<DipoleEcommerceContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);
            builder.Entity<ProductRating>()
        .HasOne(pr => pr.ProductReview)
        .WithOne(pr => pr.ProductRating)
        .HasForeignKey<ProductReview>(pr => pr.ProductRatingId);
            SeedRoles(builder);


        }
        public static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" });
        }
    }
}

using DataAccess.Models.Auth;
using DataAccess.Models.Catalog;
using DataAccess.Models.Production;
using DataAccess.Models.Stripe;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataAccess.Context
{
    public class CoreFunctionalityContext(DbContextOptions<CoreFunctionalityContext> options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        #region Auth

        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<RoleType> RoleTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        #endregion

        #region Production

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Models.Production.Product> Products { get; set; }
        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<Sku> Skus { get; set; }

        #endregion

        #region Catalog

        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<ShippingRate> ShippingRates { get; set; }
        public virtual DbSet<TaxRate> TaxRates { get; set; }
        public virtual DbSet<Checkout> Orders { get; set; }

        #endregion

        #region Stripe

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Models.Stripe.Product> StripeProducts { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public virtual DbSet<CheckoutPayment> CheckoutPayments { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Auth

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.Description).HasDefaultValueSql("('')");

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((999))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<RoleType>(entity =>
            {
                entity.Property(e => e.Description).HasDefaultValueSql("('')");

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((999))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.RoleTypeParent)
                    .WithMany(p => p.InverseRoleTypeParent)
                    .HasForeignKey(d => d.RoleTypeParentId)
                    .HasConstraintName("FK_RoleType_RoleType");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((999))");

                entity.Property(e => e.IsGoogleLogin).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsFacebookLogin).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsMicrosoftLogin).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsAppleLogin).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserType");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(e => e.User)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_Role_User");

                entity.HasOne(e => e.RoleType)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(e => e.RoleTypeId)
                    .HasConstraintName("FK_Role_RoleType");

            });

            #endregion

            #region Production

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Description).HasDefaultValueSql("('')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Description).HasDefaultValueSql("('')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.Property(e => e.Description).HasDefaultValueSql("('')");

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((999))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.Property(e => e.Description).HasDefaultValueSql("('')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Models.Production.Product>(entity =>
            {
                entity.Property(e => e.Description).HasDefaultValueSql("('')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Brand");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductType");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.Property(e => e.Description).HasDefaultValueSql("('')");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.MediaType)
                .WithMany(p => p.Medias)
                .HasForeignKey(d => d.MediaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Media_MediaType");
            });

            modelBuilder.Entity<Sku>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSkus)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sku_Product");
            });

            #endregion

            #region Catalog

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ShippingRate>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TaxRate>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Checkout>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Checkouts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checkout_Product");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.Checkouts)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checkout_Coupon");

                entity.HasOne(d => d.ShippingRate)
                    .WithMany(p => p.Checkouts)
                    .HasForeignKey(d => d.ShippingRateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checkout_ShippingRate");

                entity.HasOne(d => d.TaxRate)
                    .WithMany(p => p.Checkouts)
                    .HasForeignKey(d => d.TaxRateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Checkout_TaxRate");
            });

            #endregion

            #region Stripe

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(x => x.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_User");
            });

            modelBuilder.Entity<Models.Stripe.Product>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ProductionProduct)
                    .WithOne(p => p.StripeProduct)
                    .HasForeignKey<Models.Stripe.Product>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Product");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Description).HasDefaultValueSql("('')");

                entity.HasOne(d => d.PaymentStatus)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_PaymentStatus");
            });

            modelBuilder.Entity<PaymentStatus>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((999))");

                entity.Property(e => e.Description).HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<CheckoutPayment>(entity =>
            {
                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Checkout)
                    .WithMany(p => p.CheckoutPayments)
                    .HasForeignKey(d => d.CheckoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderPayment_Order");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.CheckoutPayments)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderPayment_Payment");
            });

            #endregion
        }
    }
}
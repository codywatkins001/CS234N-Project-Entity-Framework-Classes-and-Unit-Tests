using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BITS.Models.Models
{
    public partial class BitsContext : DbContext
    {
        public BitsContext()
        {
        }

        public BitsContext(DbContextOptions<BitsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<AddressType> AddressTypes { get; set; } = null!;
        public virtual DbSet<Adjunct> Adjuncts { get; set; } = null!;
        public virtual DbSet<AdjunctType> AdjunctTypes { get; set; } = null!;
        public virtual DbSet<AppConfig> AppConfigs { get; set; } = null!;
        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;
        public virtual DbSet<Barrel> Barrels { get; set; } = null!;
        public virtual DbSet<Batch> Batches { get; set; } = null!;
        public virtual DbSet<BatchContainer> BatchContainers { get; set; } = null!;
        public virtual DbSet<BrewContainer> BrewContainers { get; set; } = null!;
        public virtual DbSet<ContainerSize> ContainerSizes { get; set; } = null!;
        public virtual DbSet<ContainerStatus> ContainerStatuses { get; set; } = null!;
        public virtual DbSet<ContainerType> ContainerTypes { get; set; } = null!;
        public virtual DbSet<Equipment> Equipment { get; set; } = null!;
        public virtual DbSet<Fermentable> Fermentables { get; set; } = null!;
        public virtual DbSet<FermentableType> FermentableTypes { get; set; } = null!;
        public virtual DbSet<Hop> Hops { get; set; } = null!;
        public virtual DbSet<HopType> HopTypes { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<IngredientInventoryAddition> IngredientInventoryAdditions { get; set; } = null!;
        public virtual DbSet<IngredientInventorySubtraction> IngredientInventorySubtractions { get; set; } = null!;
        public virtual DbSet<IngredientType> IngredientTypes { get; set; } = null!;
        public virtual DbSet<InventoryTransaction> InventoryTransactions { get; set; } = null!;
        public virtual DbSet<InventoryTransactionType> InventoryTransactionTypes { get; set; } = null!;
        public virtual DbSet<Mash> Mashes { get; set; } = null!;
        public virtual DbSet<MashStep> MashSteps { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductContainerInventory> ProductContainerInventories { get; set; } = null!;
        public virtual DbSet<ProductContainerSize> ProductContainerSizes { get; set; } = null!;
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;
        public virtual DbSet<Style> Styles { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<SupplierAddress> SupplierAddresses { get; set; } = null!;
        public virtual DbSet<UnitType> UnitTypes { get; set; } = null!;
        public virtual DbSet<UseDuring> UseDurings { get; set; } = null!;
        public virtual DbSet<Yeast> Yeasts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=bits;user=root;password=deadmoon5440", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.5.0-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Adjunct>(entity =>
            {
                entity.HasKey(e => e.IngredientId)
                    .HasName("PRIMARY");

                entity.Property(e => e.IngredientId).ValueGeneratedNever();

                entity.Property(e => e.BatchVolume).HasDefaultValueSql("'0'");

                entity.Property(e => e.RecommendedQuantity).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.AdjunctType)
                    .WithMany(p => p.Adjuncts)
                    .HasForeignKey(d => d.AdjunctTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("adjunct_adjunct_type_FK");

                entity.HasOne(d => d.Ingredient)
                    .WithOne(p => p.Adjunct)
                    .HasForeignKey<Adjunct>(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("adjunct_ingredient_FK");

                entity.HasOne(d => d.RecommendedUseDuring)
                    .WithMany(p => p.Adjuncts)
                    .HasForeignKey(d => d.RecommendedUseDuringId)
                    .HasConstraintName("adjunct_use_during_FK");
            });

            modelBuilder.Entity<AppConfig>(entity =>
            {
                entity.HasKey(e => e.BreweryId)
                    .HasName("PRIMARY");

                entity.Property(e => e.BreweryId).ValueGeneratedNever();

                entity.Property(e => e.DefaultUnits).HasDefaultValueSql("'metric'");
            });

            modelBuilder.Entity<Barrel>(entity =>
            {
                entity.HasKey(e => e.BrewContainerId)
                    .HasName("PRIMARY");

                entity.Property(e => e.BrewContainerId).ValueGeneratedNever();

                entity.HasOne(d => d.BrewContainer)
                    .WithOne(p => p.Barrel)
                    .HasForeignKey<Barrel>(d => d.BrewContainerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("barrel_brew_container_FK");
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("batch_equipment_FK");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("batch_recipe_FK");
            });

            modelBuilder.Entity<BatchContainer>(entity =>
            {
                entity.HasKey(e => new { e.BatchId, e.BrewContainerId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchContainers)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("batch_container_batch_FK");

                entity.HasOne(d => d.BrewContainer)
                    .WithMany(p => p.BatchContainers)
                    .HasForeignKey(d => d.BrewContainerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("batch_container_brew_container_FK");
            });

            modelBuilder.Entity<BrewContainer>(entity =>
            {
                entity.HasOne(d => d.ContainerSize)
                    .WithMany(p => p.BrewContainers)
                    .HasForeignKey(d => d.ContainerSizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brew_container_container_size");

                entity.HasOne(d => d.ContainerStatus)
                    .WithMany(p => p.BrewContainers)
                    .HasForeignKey(d => d.ContainerStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brew_container_container_status_FK");

                entity.HasOne(d => d.ContainerType)
                    .WithMany(p => p.BrewContainers)
                    .HasForeignKey(d => d.ContainerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("brew_container_container_type_FK");
            });

            modelBuilder.Entity<Fermentable>(entity =>
            {
                entity.HasKey(e => e.IngredientId)
                    .HasName("PRIMARY");

                entity.Property(e => e.IngredientId).ValueGeneratedNever();

                entity.HasOne(d => d.FermentableType)
                    .WithMany(p => p.Fermentables)
                    .HasForeignKey(d => d.FermentableTypeId)
                    .HasConstraintName("fermentable_fermentable_type_FK");

                entity.HasOne(d => d.Ingredient)
                    .WithOne(p => p.Fermentable)
                    .HasForeignKey<Fermentable>(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fermentable_ingredient_FK");
            });

            modelBuilder.Entity<Hop>(entity =>
            {
                entity.HasKey(e => e.IngredientId)
                    .HasName("PRIMARY");

                entity.Property(e => e.IngredientId).ValueGeneratedNever();

                entity.HasOne(d => d.HopType)
                    .WithMany(p => p.Hops)
                    .HasForeignKey(d => d.HopTypeId)
                    .HasConstraintName("hop_hop_type");

                entity.HasOne(d => d.Ingredient)
                    .WithOne(p => p.Hop)
                    .HasForeignKey<Hop>(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("hop_ingredient_FK");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasOne(d => d.IngredientType)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.IngredientTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_ingredient_type_FK");

                entity.HasOne(d => d.UnitType)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.UnitTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_unit_type_FK");

                entity.HasMany(d => d.Ingredients)
                    .WithMany(p => p.SubstituteIngredients)
                    .UsingEntity<Dictionary<string, object>>(
                        "IngredientSubstitute",
                        l => l.HasOne<Ingredient>().WithMany().HasForeignKey("IngredientId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("ingredient_substitute_ingredient_FK"),
                        r => r.HasOne<Ingredient>().WithMany().HasForeignKey("SubstituteIngredientId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("ingredient_substitute_substitute_ingredient_FK"),
                        j =>
                        {
                            j.HasKey("IngredientId", "SubstituteIngredientId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("ingredient_substitute");

                            j.HasIndex(new[] { "SubstituteIngredientId" }, "ingredient_substitute_substitute_ingredient_FK_idx");

                            j.IndexerProperty<int>("IngredientId").HasColumnName("ingredient_id");

                            j.IndexerProperty<int>("SubstituteIngredientId").HasColumnName("substitute_ingredient_id");
                        });

                entity.HasMany(d => d.Styles)
                    .WithMany(p => p.IngredientsNavigation)
                    .UsingEntity<Dictionary<string, object>>(
                        "IngredientUsedIn",
                        l => l.HasOne<Style>().WithMany().HasForeignKey("StyleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("used_in_style_type_FK"),
                        r => r.HasOne<Ingredient>().WithMany().HasForeignKey("IngredientId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("used_in_ingredient_FK"),
                        j =>
                        {
                            j.HasKey("IngredientId", "StyleId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("ingredient_used_in");

                            j.HasIndex(new[] { "StyleId" }, "usedin_style_type_FK_idx");

                            j.IndexerProperty<int>("IngredientId").HasColumnName("ingredient_id");

                            j.IndexerProperty<int>("StyleId").HasColumnName("style_id");
                        });

                entity.HasMany(d => d.SubstituteIngredients)
                    .WithMany(p => p.Ingredients)
                    .UsingEntity<Dictionary<string, object>>(
                        "IngredientSubstitute",
                        l => l.HasOne<Ingredient>().WithMany().HasForeignKey("SubstituteIngredientId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("ingredient_substitute_substitute_ingredient_FK"),
                        r => r.HasOne<Ingredient>().WithMany().HasForeignKey("IngredientId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("ingredient_substitute_ingredient_FK"),
                        j =>
                        {
                            j.HasKey("IngredientId", "SubstituteIngredientId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("ingredient_substitute");

                            j.HasIndex(new[] { "SubstituteIngredientId" }, "ingredient_substitute_substitute_ingredient_FK_idx");

                            j.IndexerProperty<int>("IngredientId").HasColumnName("ingredient_id");

                            j.IndexerProperty<int>("SubstituteIngredientId").HasColumnName("substitute_ingredient_id");
                        });
            });

            modelBuilder.Entity<IngredientInventoryAddition>(entity =>
            {
                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.IngredientInventoryAdditions)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_inventory_addition_ingredient_FK");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.IngredientInventoryAdditions)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_invertory_addition_supplier_FK");
            });

            modelBuilder.Entity<IngredientInventorySubtraction>(entity =>
            {
                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.IngredientInventorySubtractions)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("ingredient_purchased_batch_FK");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.IngredientInventorySubtractions)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_inventory_subtraction_ingredient_FK");
            });

            modelBuilder.Entity<InventoryTransaction>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.InventoryTransactions)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_account");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.InventoryTransactions)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_app_user_FK");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.InventoryTransactions)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_batch_FK");

                entity.HasOne(d => d.InventoryTransctionType)
                    .WithMany(p => p.InventoryTransactions)
                    .HasForeignKey(d => d.InventoryTransctionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_transaction_type_FK");

                entity.HasOne(d => d.ProductContainerSize)
                    .WithMany(p => p.InventoryTransactions)
                    .HasForeignKey(d => d.ProductContainerSizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventory_transaction_product_container_size_FK");
            });

            modelBuilder.Entity<MashStep>(entity =>
            {
                entity.HasOne(d => d.Mash)
                    .WithMany(p => p.MashSteps)
                    .HasForeignKey(d => d.MashId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mast_step_mash_FK");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => new { e.BatchId, e.ProductContainerSizeId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_batch_FK");

                entity.HasOne(d => d.ProductContainerSize)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductContainerSizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_product_container_size_FK");
            });

            modelBuilder.Entity<ProductContainerInventory>(entity =>
            {
                entity.HasKey(e => e.ContainerSizeId)
                    .HasName("PRIMARY");

                entity.Property(e => e.ContainerSizeId).ValueGeneratedNever();

                entity.HasOne(d => d.ContainerSize)
                    .WithOne(p => p.ProductContainerInventory)
                    .HasForeignKey<ProductContainerInventory>(d => d.ContainerSizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_container_inventory_product_container_FK");
            });

            modelBuilder.Entity<ProductContainerSize>(entity =>
            {
                entity.HasKey(e => e.ContainerSizeId)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(e => e.ActualEfficiency).HasDefaultValueSql("'0'");

                entity.Property(e => e.BoilTime).HasDefaultValueSql("'0'");

                entity.Property(e => e.BoilVolume).HasDefaultValueSql("'0'");

                entity.Property(e => e.Efficiency).HasDefaultValueSql("'0'");

                entity.Property(e => e.EstimatedAbv).HasDefaultValueSql("'0'");

                entity.Property(e => e.EstimatedColor).HasDefaultValueSql("'0'");

                entity.Property(e => e.EstimatedFg).HasDefaultValueSql("'0'");

                entity.Property(e => e.EstimatedOg).HasDefaultValueSql("'0'");

                entity.Property(e => e.FermentationStages).HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("recipe_equipment_FK");

                entity.HasOne(d => d.Mash)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.MashId)
                    .HasConstraintName("recipe_mash_FK");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.Recipes)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_style_FK");
            });

            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                entity.Property(e => e.Time).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_ingredient_ingredient_FK");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_ingredient_recipe_FK");

                entity.HasOne(d => d.UseDuring)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.UseDuringId)
                    .HasConstraintName("recipe_ingredient_use_during_FK");
            });

            modelBuilder.Entity<SupplierAddress>(entity =>
            {
                entity.HasKey(e => new { e.SupplierId, e.AddressId, e.AddressTypeId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.SupplierAddresses)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("supplier_address_address_FK");

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.SupplierAddresses)
                    .HasForeignKey(d => d.AddressTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("supplier_address_address_type_FK");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierAddresses)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("supplier_address_supplier_FK");
            });

            modelBuilder.Entity<Yeast>(entity =>
            {
                entity.HasKey(e => e.IngredientId)
                    .HasName("PRIMARY");

                entity.Property(e => e.IngredientId).ValueGeneratedNever();

                entity.HasOne(d => d.Ingredient)
                    .WithOne(p => p.Yeast)
                    .HasForeignKey<Yeast>(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("yeast_ingredient_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

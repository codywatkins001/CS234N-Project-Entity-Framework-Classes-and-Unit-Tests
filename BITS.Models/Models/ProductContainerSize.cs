using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BITS.Models.Models
{
    [Table("product_container_size")]
    [Index("Name", Name = "name_UNIQUE", IsUnique = true)]
    public partial class ProductContainerSize
    {
        public ProductContainerSize()
        {
            InventoryTransactions = new HashSet<InventoryTransaction>();
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("container_size_id")]
        public int ContainerSizeId { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Column("volume")]
        public double Volume { get; set; }
        [Column("item_quantity")]
        public int ItemQuantity { get; set; }

        [InverseProperty("ContainerSize")]
        public virtual ProductContainerInventory? ProductContainerInventory { get; set; }
        [InverseProperty("ProductContainerSize")]
        public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; }
        [InverseProperty("ProductContainerSize")]
        public virtual ICollection<Product> Products { get; set; }
    }
}

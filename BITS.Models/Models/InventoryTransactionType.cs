using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BITS.Models.Models
{
    [Table("inventory_transaction_type")]
    [Index("Name", Name = "name_UNIQUE", IsUnique = true)]
    public partial class InventoryTransactionType
    {
        public InventoryTransactionType()
        {
            InventoryTransactions = new HashSet<InventoryTransaction>();
        }

        [Key]
        [Column("inventory_transaction_type_id")]
        public int InventoryTransactionTypeId { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [InverseProperty("InventoryTransctionType")]
        public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; }
    }
}

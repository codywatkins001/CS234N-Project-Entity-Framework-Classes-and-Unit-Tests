using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BITS.Models.Models
{
    [Table("unit_type")]
    [Index("Name", Name = "name_UNIQUE", IsUnique = true)]
    public partial class UnitType
    {
        public UnitType()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        [Key]
        [Column("unit_type_id")]
        public int UnitTypeId { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [InverseProperty("UnitType")]
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}

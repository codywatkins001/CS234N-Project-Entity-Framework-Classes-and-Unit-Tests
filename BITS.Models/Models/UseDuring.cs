using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BITS.Models.Models
{
    [Table("use_during")]
    [Index("Name", Name = "name_UNIQUE", IsUnique = true)]
    public partial class UseDuring
    {
        public UseDuring()
        {
            Adjuncts = new HashSet<Adjunct>();
            RecipeIngredients = new HashSet<RecipeIngredient>();
        }

        [Key]
        [Column("use_during_id")]
        public int UseDuringId { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [InverseProperty("RecommendedUseDuring")]
        public virtual ICollection<Adjunct> Adjuncts { get; set; }
        [InverseProperty("UseDuring")]
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}

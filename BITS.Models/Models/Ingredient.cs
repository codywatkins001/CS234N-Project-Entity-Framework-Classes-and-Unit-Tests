using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BITS.Models.Models
{
    [Table("ingredient")]
    [Index("IngredientTypeId", Name = "ingredient_ingredient_type_FK_idx")]
    [Index("UnitTypeId", Name = "ingredient_unit_type_FK_idx")]
    public partial class Ingredient
    {
        public Ingredient()
        {
            IngredientInventoryAdditions = new HashSet<IngredientInventoryAddition>();
            IngredientInventorySubtractions = new HashSet<IngredientInventorySubtraction>();
            RecipeIngredients = new HashSet<RecipeIngredient>();
            Ingredients = new HashSet<Ingredient>();
            Styles = new HashSet<Style>();
            SubstituteIngredients = new HashSet<Ingredient>();
        }

        [Key]
        [Column("ingredient_id")]
        public int IngredientId { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Column("version")]
        public int? Version { get; set; }
        [Column("ingredient_type_id")]
        public int IngredientTypeId { get; set; }
        [Column("on_hand_quantity")]
        public double OnHandQuantity { get; set; }
        [Column("unit_type_id")]
        public int UnitTypeId { get; set; }
        [Column("unit_cost")]
        [Precision(10, 2)]
        public decimal UnitCost { get; set; }
        [Column("reorder_point")]
        public double ReorderPoint { get; set; }
        [Column("notes")]
        [StringLength(2000)]
        public string? Notes { get; set; }

        [ForeignKey("IngredientTypeId")]
        [InverseProperty("Ingredients")]
        public virtual IngredientType IngredientType { get; set; } = null!;
        [ForeignKey("UnitTypeId")]
        [InverseProperty("Ingredients")]
        public virtual UnitType UnitType { get; set; } = null!;
        [InverseProperty("Ingredient")]
        public virtual Adjunct? Adjunct { get; set; }
        [InverseProperty("Ingredient")]
        public virtual Fermentable? Fermentable { get; set; }
        [InverseProperty("Ingredient")]
        public virtual Hop? Hop { get; set; }
        [InverseProperty("Ingredient")]
        public virtual Yeast? Yeast { get; set; }
        [InverseProperty("Ingredient")]
        public virtual ICollection<IngredientInventoryAddition> IngredientInventoryAdditions { get; set; }
        [InverseProperty("Ingredient")]
        public virtual ICollection<IngredientInventorySubtraction> IngredientInventorySubtractions { get; set; }
        [InverseProperty("Ingredient")]
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        [ForeignKey("SubstituteIngredientId")]
        [InverseProperty("SubstituteIngredients")]
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        [ForeignKey("IngredientId")]
        [InverseProperty("IngredientsNavigation")]
        public virtual ICollection<Style> Styles { get; set; }
        [ForeignKey("IngredientId")]
        [InverseProperty("Ingredients")]
        public virtual ICollection<Ingredient> SubstituteIngredients { get; set; }
    }
}

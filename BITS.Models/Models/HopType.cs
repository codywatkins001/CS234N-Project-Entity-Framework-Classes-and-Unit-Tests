using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BITS.Models.Models
{
    [Table("hop_type")]
    [Index("Name", Name = "name_UNIQUE", IsUnique = true)]
    public partial class HopType
    {
        public HopType()
        {
            Hops = new HashSet<Hop>();
        }

        [Key]
        [Column("hop_type_id")]
        public int HopTypeId { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string? Name { get; set; }

        [InverseProperty("HopType")]
        public virtual ICollection<Hop> Hops { get; set; }
    }
}

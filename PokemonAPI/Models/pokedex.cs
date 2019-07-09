namespace PokemonAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pokemon.pokedex")]
    public partial class pokedex
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pokedex()
        {
            pokemon_type = new HashSet<pokemon_type>();
            stats = new HashSet<stat>();
        }

        [Key]
        [StringLength(3)]
        public string pokedex_id { get; set; }

        [StringLength(100)]
        public string pokemon_name { get; set; }

        [StringLength(100)]
        public string species { get; set; }

        public double? height { get; set; }

        public double? weight { get; set; }

        [StringLength(200)]
        public string image { get; set; }

        [StringLength(20)]
        public string abilities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pokemon_type> pokemon_type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<stat> stats { get; set; }
    }
}

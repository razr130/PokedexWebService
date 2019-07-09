namespace PokemonAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pokemon.pokemon_type")]
    public partial class pokemon_type
    {
        [Key]
        public int row_id { get; set; }

        [StringLength(3)]
        public string pokedex_id { get; set; }

        public int? type_id { get; set; }

        public virtual pokedex pokedex { get; set; }

        public virtual type type { get; set; }
    }
}

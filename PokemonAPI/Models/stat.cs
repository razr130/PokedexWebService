namespace PokemonAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pokemon.stat")]
    public partial class stat
    {
        [Key]
        public int row_id { get; set; }

        [StringLength(3)]
        public string pokedex_id { get; set; }

        public int? hp { get; set; }

        public int? attack { get; set; }

        public int? defense { get; set; }

        public int? spattack { get; set; }

        public int? spdefense { get; set; }

        public int? speed { get; set; }

        public virtual pokedex pokedex { get; set; }
    }
}

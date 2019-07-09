namespace PokemonAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pokemon.move")]
    public partial class move
    {
        [Key]
        public int move_id { get; set; }

        [StringLength(25)]
        public string move_name { get; set; }

        public int? move_type { get; set; }

        [StringLength(20)]
        public string move_category { get; set; }

        public int? move_damage { get; set; }

        [StringLength(200)]
        public string move_effect { get; set; }

        public virtual type type { get; set; }
    }
}

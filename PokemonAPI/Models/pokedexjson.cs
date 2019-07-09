using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonAPI.Models
{
    public class pokedexjson
    {
        
        public string pokedex_id { get; set; }

        public string pokemon_name { get; set; }

       
        public string species { get; set; }

        public double? height { get; set; }

        public double? weight { get; set; }

       
        public string image { get; set; }

        public string abilities { get; set; }

        public string[] type { get; set; }
        public stat stat { get; set; }
    }
}
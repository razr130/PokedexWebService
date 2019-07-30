using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonAPI.Models
{
    public class movejson
    {
       
        public int move_id { get; set; }

        
        public string move_name { get; set; }

        public string move_type { get; set; }

        public string move_category { get; set; }

        public int? move_damage { get; set; }
      
        public string move_effect { get; set; }
    }
}
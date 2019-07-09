using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonAPI.Models
{
    public class Respon
    {
        public string tag { get; set; }
        public string success { get; set; }
        public string error { get; set; }
        public string token { get; set; }
    }
}
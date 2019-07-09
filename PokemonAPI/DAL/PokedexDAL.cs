using Newtonsoft.Json.Linq;
using PokemonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonAPI.DAL
{
    public class PokedexDAL : IDisposable
    {
        private PokedexModel db = new PokedexModel();

        public IQueryable<pokedex> get_pokedex()
        {
            var result = from c in db.pokedexes orderby c.pokedex_id ascending select c;
           
            return result;
        }

        public string[] addpokedex(pokedex poke, stat stat, pokemon_type[] tipe)
        {
            try
            {
                db.pokedexes.Add(poke);
                db.stats.Add(stat);
                for(int i = 0; i < tipe.Count(); i++)
                {
                    db.pokemon_type.Add(tipe[i]);
                }
                db.SaveChanges();
                return new string[] {"1", "success" };
            }
            catch(Exception x)
            {
                return new string[] { "-9", x.Message };
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

       
    }
}
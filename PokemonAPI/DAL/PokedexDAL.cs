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
        public pokedex get_pokedex_byid(string id)
        {
            var result = (from c in db.pokedexes where c.pokedex_id == id orderby c.pokedex_id ascending select c).SingleOrDefault();
            return result;
        }
        public stat get_stat_byid(string id)
        {
            var result = (from c in db.stats where c.pokedex_id == id orderby c.pokedex_id ascending select c).SingleOrDefault();
            return result;
        }
        public IQueryable<pokemon_type> get_type_byid(string id)
        {
            var result = from c in db.pokemon_type where c.pokedex_id == id orderby c.pokedex_id ascending select c;
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

        public string[] editpokedex(string id,pokedex poke, stat stats, pokemon_type[] tipe)
        {
            var resultpoke = get_pokedex_byid(id);
            var resultstat = get_stat_byid(id);
            var resulttype = get_type_byid(id).ToList();

            if (resultpoke != null)
            {
                resultpoke.pokemon_name = poke.pokemon_name;
                resultpoke.species = poke.species;
                resultpoke.height = poke.height;
                resultpoke.weight = poke.weight;
                resultpoke.image = poke.image;
                resultpoke.abilities = poke.abilities;
            }
            if(resultstat != null)
            {
                resultstat.hp = stats.hp;
                resultstat.attack = stats.attack;
                resultstat.defense = stats.defense;
                resultstat.spattack = stats.spattack;
                resultstat.spdefense = stats.spdefense;
                resultstat.speed = stats.speed;
            }
            if(resulttype != null)
            {
                for(int i = 0; i < resulttype.Count(); i++)
                {
                    resulttype[i].type_id = tipe[i].type_id;
                }
            }
            try
            {
                db.SaveChanges();
                return new string[] { "1", "success" };
            }
            catch(Exception x)
            {
                return new string[] { "-9", x.Message };
            }

        }

        public string[] deletepokedex(string id)
        {
            var result = get_pokedex_byid(id);
            if (result != null)
            {
                db.pokedexes.Remove(result);
                db.SaveChanges();
                return new string[] { "1", "success" };
            }
            else
            {
                return new string[] { "-9", "data not found" };
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

       
    }
}
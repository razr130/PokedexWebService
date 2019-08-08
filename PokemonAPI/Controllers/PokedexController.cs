using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PokemonAPI.DAL;
using PokemonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PokemonAPI.Controllers
{
    public class PokedexController : ApiController
    {

        [AllowAnonymous]
        [HttpGet]
        [Route("pokedex/get_pokedex")]
        public IHttpActionResult get_pokedex()
        {
            using (PokedexDAL poke = new PokedexDAL())
            {
                var list = poke.get_pokedex().ToList();
                pokedexjson[] pokemodel = new pokedexjson[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    pokemodel[i] = new pokedexjson();
                    pokemodel[i].pokedex_id = list[i].pokedex_id;
                    pokemodel[i].pokemon_name = list[i].pokemon_name;
                    pokemodel[i].species = list[i].species;
                    pokemodel[i].height = list[i].height;
                    pokemodel[i].weight = list[i].weight;
                    pokemodel[i].abilities = list[i].abilities;
                    pokemodel[i].image = list[i].image;
                    pokemodel[i].req_move = list[i].req_move;

                    var type = list[i].pokemon_type.ToArray();
                    string[] tipe = new string[type.Count()];
                    using (TypeDAL typedal = new TypeDAL())
                    {
                        for (int u = 0; u < type.Count(); u++)
                        {
                            tipe[u] = typedal.get_type_name(type[u].type_id);
                        }
                    }
                    pokemodel[i].type = tipe;

                    var stat = list[i].stats.ToArray();
                    stat statmodel = new stat();
                    for (int o = 0; o < stat.Count(); o++)
                    {
                        statmodel = new stat();
                        statmodel.pokedex_id = list[i].pokedex_id;
                        statmodel.hp = stat[o].hp;
                        statmodel.attack = stat[o].attack;
                        statmodel.defense = stat[o].defense;
                        statmodel.spattack = stat[o].spattack;
                        statmodel.spdefense = stat[o].spdefense;
                        statmodel.speed = stat[o].speed;
                    }
                    pokemodel[i].stat = statmodel;
                }
                return Json(pokemodel);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("pokedex/get_pokedex")]
        public IHttpActionResult get_pokedex(string id)
        {
            using (PokedexDAL poke = new PokedexDAL())
            {
                var list = poke.get_pokedex_byid(id);
                pokedexjson pokemodel = new pokedexjson();

            
                pokemodel.pokedex_id = list.pokedex_id;
                pokemodel.pokemon_name = list.pokemon_name;
                pokemodel.species = list.species;
                pokemodel.height = list.height;
                pokemodel.weight = list.weight;
                pokemodel.abilities = list.abilities;
                pokemodel.image = list.image;
                pokemodel.req_move = list.req_move;

                var type = list.pokemon_type.ToArray();
                string[] tipe = new string[type.Count()];
                using (TypeDAL typedal = new TypeDAL())
                {
                    for (int u = 0; u < type.Count(); u++)
                    {
                        tipe[u] = typedal.get_type_name(type[u].type_id);
                    }
                }
                pokemodel.type = tipe;

                var stat = list.stats.ToArray();
                stat statmodel = new stat();
                for (int o = 0; o < stat.Count(); o++)
                {
                    statmodel = new stat();
                    statmodel.hp = stat[o].hp;
                    statmodel.attack = stat[o].attack;
                    statmodel.defense = stat[o].defense;
                    statmodel.spattack = stat[o].spattack;
                    statmodel.spdefense = stat[o].spdefense;
                    statmodel.speed = stat[o].speed;
                }
                pokemodel.stat = statmodel;

                return Json(pokemodel);
            }
        }


    }
}

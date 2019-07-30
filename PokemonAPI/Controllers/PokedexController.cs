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
                        for(int u = 0; u < type.Count(); u++)
                        {
                            tipe[u] = typedal.get_type_name(type[u].type_id);
                        }                      
                    }
                    pokemodel[i].type = tipe;

                    var stat = list[i].stats.ToArray();
                    stat statmodel = new stat();
                    for(int o=0; o<stat.Count();o++)
                    {                       
                        statmodel = new stat();
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
                var list = poke.get_pokedex_byid(id).ToList();
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
        [HttpPost]
        [Route("pokedex/post_pokedex")]
        public IHttpActionResult post_pokedex(pokedexpost poke)
        {
            pokedex pokedata = Newtonsoft.Json.JsonConvert.DeserializeObject<pokedex>(poke.dexdata);
            stat statdata = Newtonsoft.Json.JsonConvert.DeserializeObject<stat>(poke.statdata);
            var type = Newtonsoft.Json.JsonConvert.DeserializeObject<List<type>>(poke.typedata);

            type[] tipe = type.ToArray();
            pokemon_type[] poktip = new pokemon_type[tipe.Count()];
            using (TypeDAL tipedal = new TypeDAL())
            {
                for (int i = 0; i < tipe.Count(); i++)
                {
                    poktip[i] = new pokemon_type();
                    poktip[i].type_id = tipedal.get_type_id(tipe[i].type1);
                    poktip[i].pokedex_id = pokedata.pokedex_id;
                }
            }
                
            statdata.pokedex_id = pokedata.pokedex_id;
            using (PokedexDAL pokedal = new PokedexDAL())
            {
                string[] respon = pokedal.addpokedex(pokedata, statdata, poktip);
                Respon res = new Respon();
                if (respon[0] == "1")
                {
                    
                        res.error = "0";
                        res.success = "1";
                        res.tag = "post pokedex";
                        res.token = "success adding";                  
                    
                }
                else
                {
                    res.error = "1";
                    res.success = "0";
                    res.tag = "post pokedex";
                    res.token = "failed adding";
                }
                return Json(res);
            }

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("pokedex/postimage")]
        public bool postimage()
        {
            bool resulted = true;
            HttpResponseMessage result = null;
            var httprequest = HttpContext.Current.Request;
            if(httprequest.Files.Count > 0)
            {
                var docfile = new List<string>();
                for (int i=0;i<httprequest.Files.Count;i++)
                {
                    var postedfile = httprequest.Files[i];
                    var filepath = HttpContext.Current.Server.MapPath("~/Content/Images/" + postedfile.FileName);
                    postedfile.SaveAs(filepath);
                    docfile.Add(filepath);
                    postedfile = null;
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfile);

            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
                resulted = false;
            }
            return resulted;
        }
    }
}

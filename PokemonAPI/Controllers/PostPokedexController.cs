﻿using PokemonAPI.DAL;
using PokemonAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PokemonAPI.Controllers
{
    public class PostPokedexController : Controller
    {
        // GET: PostPokedex
        [HttpPost]
        [Route("PostPokedex/post_dex")]
        public ActionResult post_dex(pokedexpost poke, IEnumerable<HttpPostedFileBase> image)
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


            //simpan file ke folder, belum insert string ke database
            string filepath = "";
            if (image.Count() > 0)
            {
                foreach (var pic in image)
                {
                    if (pic != null)
                    {
                        filepath = Path.Combine(HttpContext.Server.MapPath("~/Content/Images"), pic.FileName);
                        pic.SaveAs(filepath);
                        pokedata.image = pic.FileName;
                    }
                }
            }

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

        [HttpPut]
        [Route("PostPokedex/edit_dex")]
        public ActionResult edit_dex(string id, pokedexpost poke, IEnumerable<HttpPostedFileBase> image)
        {
            if (id != null)
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


                //simpan file ke folder, belum insert string ke database
                string filepath = "";
                if (image.Count() > 0)
                {
                    foreach (var pic in image)
                    {
                        if (pic != null)
                        {
                            filepath = Path.Combine(HttpContext.Server.MapPath("~/Content/Images"), pic.FileName);
                            pic.SaveAs(filepath);
                            pokedata.image = pic.FileName;
                        }
                    }
                }

                using (PokedexDAL pokedal = new PokedexDAL())
                {
                    string[] respon = pokedal.editpokedex(id, pokedata, statdata, poktip);
                    Respon res = new Respon();
                    if (respon[0] == "1")
                    {
                        res.error = "0";
                        res.success = "1";
                        res.tag = "edit pokedex";
                        res.token = "success editing";

                    }
                    else
                    {
                        res.error = "1";
                        res.success = "0";
                        res.tag = "edit pokedex";
                        res.token = "failed editing";
                    }
                    return Json(res);
                }
            }
            else
            {
                Respon res = new Respon();
                res.error = "1";
                res.success = "0";
                res.tag = "edit pokedex";
                res.token = "id is empty";
                return Json(res);
            }

        }
        [HttpDelete]
        [Route("PostPokedex/delete_dex")]
        public ActionResult delete_dex(string id)
        {
            Respon res = new Respon();
            using (PokedexDAL poke = new PokedexDAL())
            {
                try
                {
                    poke.deletepokedex(id);
                    res.error = "0";
                    res.success = "1";
                    res.tag = "delete pokedex";
                    res.token = "success deleting";
                }
                catch (Exception x)
                {
                    res.error = "1";
                    res.success = "0";
                    res.tag = "edit pokedex";
                    res.token = x.Message;
                }
            }
            return Json(res);

        }
    }
}
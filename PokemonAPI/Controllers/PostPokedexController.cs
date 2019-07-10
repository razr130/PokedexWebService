using PokemonAPI.DAL;
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
            if(image.Count() > 0)
            {                    
                foreach(var pic in image)
                {
                    if(pic != null)
                    {                        
                        filepath = Path.Combine(HttpContext.Server.MapPath("~/Content/Images"), pic.FileName);
                        pic.SaveAs(filepath);
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

        [HttpPost]
        [Route("PostPokedex/upload_file")]
        public ActionResult upload_file(IEnumerable<HttpPostedFileBase> image)
        {
            Respon res = new Respon();
            string filepath = "";
            try { 
            if (image.Count() > 0)
            {
                foreach (var pic in image)
                {
                    if (pic != null)
                    {
                        filepath = Path.Combine(HttpContext.Server.MapPath("~/Content/Images"), pic.FileName);
                        pic.SaveAs(filepath);
                    }
                }
            }
                res.error = "0";
                res.success = "1";
                res.tag = "upload";
                res.token = "success upload";
                return Json(res);
            }
            catch(Exception x)
            {
                res.error = "1";
                res.success = "0";
                res.tag = "upload";
                res.token = x.ToString();
                return Json(res);
            }

        }


    }
}
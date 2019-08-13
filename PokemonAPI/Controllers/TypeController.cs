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
    public class TypeController : ApiController
    {
        // GET: Type
        [AllowAnonymous]
        [HttpGet]
        [Route("type/get_type")]
        public IHttpActionResult get_type()
        {
            using (TypeDAL type = new TypeDAL())
            {
                var list = type.get_type().ToList();
                typejson[] pokemodel = new typejson[list.Count()];
               
                for (int i = 0; i < list.Count; i++)
                {
                    pokemodel[i] = new typejson();
                    pokemodel[i].type_name = list[i].type1;                
                }
                return Json(pokemodel);
            }
        }

    }
}
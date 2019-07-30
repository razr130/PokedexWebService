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
    public class MoveController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("move/get_move")]
        public IHttpActionResult get_move()
        {
            using (MoveDAL movedal = new MoveDAL())
            {
                try
                {
                    var list = movedal.get_move().ToList();
                    movejson[] movedata = new movejson[list.Count()];

                    for (int i = 0; i < list.Count(); i++)
                    {
                        movedata[i] = new movejson();
                        movedata[i].move_id = list[i].move_id;
                        movedata[i].move_name = list[i].move_name;
                        using (TypeDAL type = new TypeDAL())
                        {
                            movedata[i].move_type = type.get_type_name(list[i].move_type);
                        }
                        movedata[i].move_category = list[i].move_category;
                        movedata[i].move_damage = list[i].move_damage;
                        movedata[i].move_effect = list[i].move_effect;
                    }
                    return Json(movedata);
                }
                catch(Exception x)
                {
                    Respon respon = new Respon();
                    respon.success = "0";
                    respon.error = "1";
                    respon.tag = "get move";
                    respon.token = x.Message;

                    return Json(respon);
                }
            }
        }

    }
}
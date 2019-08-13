using PokemonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonAPI.DAL
{
    public class TypeDAL : IDisposable
    {
        private PokedexModel db = new PokedexModel();

        public IQueryable<type> get_type()
        {
            var result = from c in db.types
                         orderby c.type_id ascending
                         select c;
            return result;
        }

        public string get_type_name(int? id)
        {
            var name = (from c in db.types where c.type_id == id select c.type1).SingleOrDefault();
            return name;
        }
        public int get_type_id(string name)
        {
            var id = (from c in db.types where c.type1 == name select c.type_id).SingleOrDefault();
            return id;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
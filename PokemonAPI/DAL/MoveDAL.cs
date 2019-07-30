using PokemonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonAPI.DAL
{
    public class MoveDAL : IDisposable
    {
        private PokedexModel db = new PokedexModel();

        public IQueryable<move> get_move()
        {
            var result = from c in db.moves orderby c.move_id ascending select c;

            return result;
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IMDBWEBAPI.Models;
using LiteDB;

namespace IMDBWEBAPI.Controllers
{
    public class FilmsController : ApiController
    {
        private static FilmsModel _db = new FilmsModel();

        public IEnumerable<Film> GetAllFilms()
        {
            return _db.GetAllFilms();
        }

        public IHttpActionResult GetFilm(string id)
        {
            var film = _db.Control("title", id);
            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }

        public IHttpActionResult PostFilm(string name)
        {
            var film = _db.Search(name);
            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }
    }
}

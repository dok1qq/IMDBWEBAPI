using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IMDBWEBAPI.Models;

namespace IMDBWEBAPI.Controllers
{
    public class FilmsController : ApiController
    {
        private static FilmsModel _db = new FilmsModel();

        public IEnumerable<Film> GetAllFilms()
        {
            return _db.GetAllFilms();
        }

        [HttpGet]
        public IHttpActionResult GetFilm(string id)
        {
            var film = _db.Control("title", id);
            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }

        [HttpPost]
        public IHttpActionResult PostFilm()
        {
            var text = Request.Content.ReadAsStringAsync();
            var film = _db.Search(text.Result);
            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }
    }
}

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
        public IEnumerable<Film> GetAllFilms()
        {
            return FilmsModel.GetAllFilms();
        }

        [HttpGet]
        public Film GetFilm(string id)
        {
            var film = FilmsModel.Control("title", id);
            if (film == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return film;
        }

        [HttpPost]
        public Film PostFilm()
        {
            var text = Request.Content.ReadAsStringAsync();
            var film = FilmsModel.Search(text.Result);
            if (film == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return film;
        }
    }
}

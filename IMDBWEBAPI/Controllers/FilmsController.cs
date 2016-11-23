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
        
        Film[] films = new Film[]
        {
            new Film { Id = "1", Creator = "Tomato Soup", Description = "Groceries", Poster = "1", Title = "First title"},
            new Film { Id = "2", Creator = "Yo-yo", Description = "Toys", Poster = "1", Title = "Second title" },
            new Film { Id = "3", Creator = "Hammer", Description = "Hardware", Poster = "1", Title = "Third title" }
        };

        public IEnumerable<Film> GetAllFilms()
        {
            return films;
        }

        public IHttpActionResult GetFilm(string id)
        {
            var film = films.FirstOrDefault((f) => f.Id == id);
            if (film == null)
            {
                return NotFound();
            }
            return Ok(film);
        }
    }
}

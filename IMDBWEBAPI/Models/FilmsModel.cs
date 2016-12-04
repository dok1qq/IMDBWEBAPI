using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LiteDB;
using HtmlAgilityPack;

namespace IMDBWEBAPI.Models
{
    public static class FilmsModel
    {
        private static string title = "//*[@itemprop='name']";
        private static string rating = "//*[@itemprop='ratingValue']";
        private static string description = "//*[@itemprop='description']";
        private static string creator = "//*[@itemprop='creator']/a/span";
        private static string fImage = "//*[@class='poster']/a";
        private static string nImage = "//*[@class='image']/a";

        public static void SetFilmInDB(Film film)
        {
            using (var db = new LiteDatabase(@"D:\Projects\visual studio 2015\IMDBWEBAPI\IMDBWEBAPI\bin\Films.db"))
            {
                var col = db.GetCollection<Film>("films");
                col.Insert(film);
            }
        }

        public static Film GetFilmFromDB(string id)
        {
            using (var db = new LiteDatabase(@"D:\Projects\visual studio 2015\IMDBWEBAPI\IMDBWEBAPI\bin\Films.db"))
            {
                var col = db.GetCollection<Film>("films");
                return col.FindOne(film => film.Id == id);
            }
        }

        public static IEnumerable<Film> GetAllFilms()
        {
            using (var db = new LiteDatabase(@"D:\Projects\visual studio 2015\IMDBWEBAPI\IMDBWEBAPI\bin\Films.db"))
            {
                var col = db.GetCollection<Film>("films");
                return col.FindAll();
            }
        }

        public static Film Control(string property, string id)
        {
            Film film = GetFilmFromDB(id);

            if (film == null)
            {
                film = GetInfoOfFilm(property, id);
                if (film != null)
                {
                    SetFilmInDB(film);
                }
            }

            return film;
        }

        public static Film Search(string str)
        {
            HtmlWeb web = new HtmlWeb();

            HtmlDocument doc = web.Load("http://www.imdb.com/find?ref_=nv_sr_fn&q=" + str + "&s=all");
            if (doc != null)
            {
                HtmlNode cell = doc.DocumentNode.SelectSingleNode("//*[@class='findList']").SelectSingleNode("tr").SelectSingleNode("th|td");
                string text = cell.InnerHtml;
                string[] substrings = text.Split('/');

                return Control(substrings[1], substrings[2]);
            }
            return null;
        }

        private static Film GetInfoOfFilm(string property, string _id)
        {
            HtmlWeb webId = new HtmlWeb();

            HtmlAgilityPack.HtmlDocument doc = webId.Load("http://www.imdb.com/" + property + "/" + _id);

            if (doc != null)
            {
                string recieveTitle = "";
                string recieveRating = "";
                string recieveDescription = "";
                string recieveCreator = "";
                string resultUrlImage = "";

                HtmlNode recTitle = doc.DocumentNode.SelectSingleNode(title);
                if (recTitle != null)
                {
                    recieveTitle = recTitle.InnerText;
                }

                HtmlNode recRating = doc.DocumentNode.SelectSingleNode(rating);
                if (recRating != null)
                {
                    recieveRating = recRating.InnerText;
                }

                HtmlNode recDescription = doc.DocumentNode.SelectSingleNode(description);
                if (recDescription != null)
                {
                    recieveDescription = recDescription.InnerText;
                }

                HtmlNode recCreator = doc.DocumentNode.SelectSingleNode(creator);
                if (recCreator != null)
                {
                    recieveCreator = recCreator.InnerHtml;
                }

                HtmlNode recImg = doc.DocumentNode.SelectSingleNode(fImage);
                if ((property.Equals("title")) && (recImg != null))
                {
                    string sImg = recImg.InnerHtml;
                    string[] substrings = sImg.Split('"');
                    resultUrlImage = substrings[5];
                }

                if ((property.Equals("name")) && (recImg != null))
                {
                    string sImg = doc.DocumentNode.SelectSingleNode(nImage).InnerHtml;
                    string[] substrings = sImg.Split('"');
                    resultUrlImage = substrings[11];
                }

                return new Film()
                {
                    Id = _id,
                    Title = recieveTitle,
                    Rating = recieveRating,
                    Director = recieveCreator,
                    Description = recieveDescription,
                    Poster = resultUrlImage
                };
            }
            else
            {
                return null;
            }
        }
    }
}
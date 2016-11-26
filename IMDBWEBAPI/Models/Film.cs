﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDBWEBAPI.Models
{
    public class Film
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Poster { get; set; }
        public string Rating { get; set; }
    }
}
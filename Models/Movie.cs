using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGH_movie_rent.Models
{
    public class Movie
    {
       
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int ReleaseYear { get; set; }

        public string PricePerDay { get; set; }

        public string Producer { get; set;  }

       
    }
}

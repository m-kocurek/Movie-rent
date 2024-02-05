using System;
using System.ComponentModel;
using AGH_movie_rent.Models;

namespace AGH_movie_rent.Models
{
    public class MovieRental
    {
        public int MovieRentalId { get; set; }


        [DisplayName("User name")]
        public string UserId { get; set; }

        public string UserName { get; set; }


        [DisplayName("Movie")]
        public string MovieId { get; set; }
        public string MovieName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [DisplayName("Charged Fee")]
        public int ChargedFee { get; set; }

        public string Status {get; set; } //paid, not  paid , during the rent period

    }
}

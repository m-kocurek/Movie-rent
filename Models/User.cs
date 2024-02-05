using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace AGH_movie_rent.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [DisplayName("Phone number")]
        public int PhoneNumber { get; set; }

        public string Email { get; set; }

        [DisplayName("Password")]
        public string HashedPasswd {get; set; }

        public int Pesel { get; set; }

        public string Role { get; set; }
    }
}

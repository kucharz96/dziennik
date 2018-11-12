using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public abstract class Profil
    {
        public int ID { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string login { get; set;}
        public string haslo { get; set; }


    }
}
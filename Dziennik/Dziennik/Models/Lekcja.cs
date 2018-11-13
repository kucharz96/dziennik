using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Lekcja
    {
        public int ID { get; set; }
        public int IDPrzedmiot { get; set; }
        public int IDKlasa { get; set; }
        public DateTime date { get; set; }

        public virtual Przedmiot Przedmiot { get; set; }
        public virtual Klasa Klasa { get; set; }

        public virtual ICollection<Spoznienie> Spoznienia { get; set; }
        public virtual ICollection<Nieobecnosc> Nieobecnosci { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Nauczyciel
    {
        public int ID { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string login { get; set; }
        public string haslo { get; set; }


        public virtual ICollection<Klasa> Klasy { get; set; }
        public virtual ICollection<Przedmiot> Przedmioty { get; set; }
        public virtual ICollection<Plik> Pliki { get; set; }
        public virtual ICollection<Test> Testy { get; set; }
        public virtual ICollection<Uwaga> Uwagi { get; set; }
        public virtual ICollection<Ogloszenie> Ogloszenia { get; set; }
        public virtual ICollection<Ogloszenie_do_rodzicow>Ogloszenia_r { get; set; }
        public virtual ICollection<Ocena> Oceny { get; set; }
        public virtual Nauczyciel Wychowawca { get; set; }
    }
}
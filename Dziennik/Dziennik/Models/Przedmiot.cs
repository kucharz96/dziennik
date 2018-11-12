using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public enum klasa
    {
        1, 2, 3
    }
    public class Przedmiot

    {
        public int ID { get; set; }
        public string nazwa { get; set; }
        public klasa? level { get; set; }

        public virtual Przedmiot Przedmiot { get; set; }
        public virtual ICollection<Klasa> Klasy { get; set; }
        public virtual ICollection<Nauczyciel> Nauczyciele { get; set; }
        public virtual ICollection<Ocena> Oceny { get; set; }
        public virtual ICollection<Lekcja> Lekcje { get; set; }
        public virtual ICollection<Plik> Pliki { get; set; }
        public virtual ICollection<Test> Testy { get; set; }

    }

}
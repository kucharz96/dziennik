using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Plik
    {
        public int ID { get; set; }
        public int IDPrzedmiot { get; set; }
        public int IDKlasa { get; set; }
        public int IDNauczyciel { get; set; }
        

        public virtual Przedmiot Przedmiot { get; set; }
        public virtual Klasa Klasa { get; set; }
        public virtual Nauczyciel Nauczyciel { get; set; }

        
    }
}
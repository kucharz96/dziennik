using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    
    public class Ocena
    {
        public int ID { get; set; }
        public int ocena { get; set; }
        public int waga { get; set; }
        public DateTime data { get; set; }
        public string tresc { get; set; }
        public int IDPrzedmiot { get; set; }
        public int IDNauczyciel { get; set; }
        public int IDUczen { get; set; }

        public virtual Uczen Uczen { get; set; }
        public virtual Nauczyciel Nauczyciel { get; set; }
        public virtual Przedmiot Przedmiot { get; set; }
    }
}
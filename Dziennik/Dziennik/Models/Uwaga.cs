using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Uwaga
    {
        public int ID { get; set; }
        public int IDNauczyciel { get; set; }
        public int IDUczen { get; set; }
        public string naglowek { get; set; }
        public string tresc { get; set; }
        public DateTime date { get; set; }

        public virtual Nauczyciel Nauczyciel { get; set; }
        public virtual Uczen Uczen { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Test
    {
        public int ID { get; set; }
        public int IDPrzedmiot { get; set; }
        public DateTime czas_trwania { get; set; }

        public virtual Przedmiot Przedmiot { get; set; }
    }
}
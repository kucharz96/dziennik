using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dziennik.Models
{
    public class Efekt_ksztalcenia
    {
        public int ID { get; set; }
        [Key]
        [ForeignKey("Przedmiot")]
        public int IDPrzedmiot { get; set; }
        public File plik { get; set; }

        public virtual Przedmiot Przedmiot { get; set; }
        

    }

}
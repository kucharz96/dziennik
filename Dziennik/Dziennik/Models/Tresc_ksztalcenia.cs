using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Dziennik.Models
{
    public class Tresc_ksztalcenia
    {
        public int ID { get; set; }
        [Key]
        [ForeignKey("Przedmiot")]
        public int IDPrzedmiot { get; set; }
        public FileStream plik { get; set; }

        public virtual Przedmiot Przedmiot { get; set; }
        

    }

}
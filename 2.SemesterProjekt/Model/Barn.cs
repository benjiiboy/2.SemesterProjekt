using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Model
{
   public class Barn
    {
        public int ID { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        //husk år først altså fx 19921210 for 12 10 1992
        public int Fødselsdato { get; set; }
        public int TelefonNr { get; set; }
    }
}

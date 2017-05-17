using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Model
{
   public class Skema
    {
        public int Vac_Id { get; set; }
        public DateTime Tid { get; set; }
        public string VaccineNavn { get; set; }
        public string Note { get; set; }


        public Skema(int vac, DateTime vacinetid, string vacinenavn, string note)
        {
            this.Vac_Id = vac;
            this.Tid = vacinetid;
            this.VaccineNavn = vacinenavn;
            this.Note = note;
        }



    }
}

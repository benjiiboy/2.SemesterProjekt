using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Model
{
   public class Vaccine
    {
        public int Vac_Id { get; set; }
        public int Tid { get; set; }
        public string VaccineNavn { get; set; }
        public string Note { get; set; }


        public Vaccine(int vac, int vacinetid, string vacinenavn, string note)
        {
            this.Vac_Id = vac;
            this.Tid = vacinetid;
            this.VaccineNavn = vacinenavn;
            this.Note = note;
        }
    }
}

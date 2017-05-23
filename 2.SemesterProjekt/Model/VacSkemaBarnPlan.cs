using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Model
{
   public class VacSkemaBarnPlan
    {
        public int Plan_Id { get; set; }
        public bool TrueFalse { get; set; }
        public DateTime VaccineTid { get; set; }
        public int Barn_Id { get; set; }
        public int Vac_Id { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public DateTime Fødselsdato { get; set; }
        public int Tid { get; set; }
        public string VaccineNavn { get; set; }
        public string VaccineModtaget { get; set; }


        public VacSkemaBarnPlan(int vacid, int barnid, DateTime vaccinetid, string fornavn, string efternavn, DateTime Føds, bool tf)
        {
            this.Vac_Id = vacid;
            this.Barn_Id = barnid;
            this.VaccineTid = vaccinetid;
            this.Fornavn = fornavn;
            this.Efternavn = efternavn;
            this.Fødselsdato = Føds;
            this.VaccineModtaget = tf ? "Ja":"Nej";
        }

        public VacSkemaBarnPlan(int vacid,int barnid, string fornavn, string efternavn, string vacnavn, bool tf, DateTime vacinetid)
        {
            this.Vac_Id = vacid;
            this.VaccineTid = vacinetid;
            this.Fornavn = fornavn;
            this.Efternavn = efternavn;
            this.VaccineNavn = vacnavn;
            this.Barn_Id = barnid;
            this.VaccineModtaget = tf ? "Ja" : "Nej";

        }

        public VacSkemaBarnPlan()
        {

        }

    }
}

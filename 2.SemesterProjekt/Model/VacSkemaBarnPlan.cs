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


        public VacSkemaBarnPlan(DateTime vaccinetid, string fornavn, string efternavn, DateTime Føds, bool tf)
        {
            this.VaccineTid = vaccinetid;
            this.Fornavn = fornavn;
            this.Efternavn = efternavn;
            this.TrueFalse = tf;
            this.Fødselsdato = Føds;
        }

        public VacSkemaBarnPlan(int tid, string fornavn, string efternavn, string vacnavn, bool tf, DateTime føds, DateTime vacinetid)
        {
            this.VaccineTid = vacinetid;
            this.Fornavn = fornavn;
            this.Efternavn = efternavn;
            this.VaccineNavn = vacnavn;
            this.TrueFalse = tf;
            this.Fødselsdato = føds;
            this.Tid = tid;
        }

        public VacSkemaBarnPlan()
        {

        }

        public override string ToString()
        {
            return $"Navn: {Fornavn} {Efternavn}, VaccineNavn: {VaccineNavn}, Tid: {VaccineTid}, Givet vaccine: {TrueFalse}"; 
        }
    }
}

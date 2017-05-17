using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Model
{
   public class PlanBarn
    {
        public int Plan_Id { get; set; }
        public DateTime Tid { get; set; }
        public bool TrueFalse { get; set; }
        public string Note { get; set; }
        public string VaccineNavn { get; set; }
        public int Barn_Id { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public DateTime Fødselsdato { get; set; }
        public int TelefonNr { get; set; }

        public PlanBarn(String fornavn, String efternavn, String vaccinenavn, bool truefalse, DateTime tid)
        {
            this.Fornavn = fornavn;
            this.Efternavn = efternavn;
            this.VaccineNavn = vaccinenavn;
            this.TrueFalse = truefalse;
            this.Tid = tid;
        }

        //public PlanBarn(int plan, DateTime tid, bool tf, string note, string vacnavn, int barn, string fornavn, string efternavn, DateTime føds, int tlf)
        //{
        //    this.Plan_Id = plan;
        //    this.Tid = tid;
        //    this.TrueFalse = tf;
        //    this.Note = note;
        //    this.VaccineNavn = vacnavn;
        //    this.Barn_Id = barn;
        //    this.Fornavn = fornavn;
        //    this.Efternavn = efternavn;
        //    this.Fødselsdato = føds;
        //    this.TelefonNr = tlf;
        //}

        //public PlanBarn()
        //{

        //}

        public override string ToString()
        {
            return $"Navn: {Fornavn} {Efternavn}, Dato: {Tid}, VacNavn: {VaccineNavn}, Fået vaccine {TrueFalse} ";
        }

    }
}

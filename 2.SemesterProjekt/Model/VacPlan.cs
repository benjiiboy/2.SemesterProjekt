using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Model
{
  public  class VacPlan
    {
        public int Plan_Id { get; set; }
        public DateTime Tid { get; set; }
        public bool TrueFalse { get; set; }
        public string Note { get; set; }
        public string VaccineNavn { get; set; }
        public int Barn_Id { get; set; }

        public VacPlan()
        {

        }

        public VacPlan(int planID, DateTime Tid, bool TrueFalse, string Note, string name, int Barn)
        {

            this.Plan_Id = planID;
            this.Tid = Tid;
            this.TrueFalse = TrueFalse;
            this.Note = Note;
            this.VaccineNavn = name;
            this.Barn_Id = Barn;

        }

        public override string ToString()
        {
            return $"{Tid} modtaget vac:{TrueFalse} note:{Note} navn på vac:{VaccineNavn}";
        }

    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Model
{
  public  class VacPlan
    {
        public int Plan_Id;
        public DateTime Tid;
        public bool TrueFalse;
        public string Note;
        public string VaccineNavn;
        public int Barn_Id;


        public VacPlan(int planID, DateTime Tid, bool TrueFalse, string Note, string VaccineNavn, int Barn_Id)
        {

            this.Plan_Id = planID;
            this.Tid = Tid;
            this.TrueFalse = TrueFalse;
            this.Note = Note;
            this.VaccineNavn = VaccineNavn;
            this.Barn_Id = Barn_Id;

        }



        public override string ToString()
        {
            return $"PlanID:{Plan_Id} {Tid} modtaget vac:{TrueFalse} note:{Note} navn på vac:{VaccineNavn}";
        }

    }
}



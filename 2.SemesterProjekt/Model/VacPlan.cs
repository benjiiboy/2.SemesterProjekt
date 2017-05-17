using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Model
{
   public class VacPlan
    {
        public int Plan_Id { get; set; }

        public DateTime VaccineTid { get; set; }

        public bool TrueFalse { get; set; }

        public int? Barn_Id { get; set; }

        public int? Vac_Id { get; set; }

        public VacPlan()
        {

        }

        public VacPlan(int plan, DateTime vaccinetid, bool tf, int barn, int vac)
        {
            this.Plan_Id = plan;
            this.VaccineTid = vaccinetid;
            this.TrueFalse = tf;
            this.Barn_Id = barn;
            this.Vac_Id = vac;
        }
    }
}

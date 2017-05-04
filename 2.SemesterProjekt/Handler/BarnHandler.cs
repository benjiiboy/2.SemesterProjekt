using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.SemesterProjekt.Viewmodel;

namespace _2.SemesterProjekt.Handler
{
   public class BarnHandler
    {

        public VaccAppViewModel VaccAppVievModel { get; set; }

        public BarnHandler(VaccAppViewModel vaccappViewModel)
        {
            VaccAppVievModel = vaccappViewModel;
        }

        public void OpretBarn()
        {
            Model.Barn tempbarn = new Model.Barn(VaccAppVievModel.ID, VaccAppVievModel.ForNavn, VaccAppVievModel.EfterNavn, VaccAppVievModel.Fødselsdato,VaccAppVievModel.TelefonNr);
            VaccAppVievModel.Singleton.TilføjBarn(tempbarn);
            VaccAppVievModel.Singleton.hent();
        }

        public void SletBarn()
        {
            VaccAppVievModel.Singleton.FjernBarn(VaccAppVievModel.SelectedBarn);
        }

        public void PutBarn()
        {
            Model.Singleton.VaccAppSingletion.PutBarn(VaccAppVievModel.SelectedBarn);
        }


    }
}

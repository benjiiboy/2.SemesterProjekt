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
            Model.Barn tempbarn = new Model.Barn();
            tempbarn.Fødselsdato = VaccAppVievModel.Fødselsdato;
            tempbarn.Fornavn = VaccAppVievModel.ForNavn;
            tempbarn.Efternavn = VaccAppVievModel.EfterNavn;

            Model.Singleton.VaccAppSingletion.TilføjBarn(tempbarn);
            _2.SemesterProjekt.Persistency.PersistencyService.PostBarn(tempbarn);
        }

        public void SletBarn()
        {
            VaccAppVievModel.Singleton.FjernBarn(VaccAppVievModel.SelectedBarn);
            //TODO: save json skal lægges ind her
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.SemesterProjekt.Viewmodel;
using Windows.UI.Popups;
using _2.SemesterProjekt.Converter;
using Windows.UI.Notifications;
using System.Xml;
using Windows.Data.Xml.Dom;

namespace _2.SemesterProjekt.Handler
{
   public class BarnHandler
    {

        public VaccAppViewModel VaccAppVievModel { get; set; }

        public BarnHandler(VaccAppViewModel vaccappViewModel)
        {
            VaccAppVievModel = vaccappViewModel;
        }

        public async void OpretBarn()
        {
            try
            {
                Model.Barn tempbarn = new Model.Barn(VaccAppVievModel.Barn_Id, VaccAppVievModel.ForNavn, VaccAppVievModel.EfterNavn, DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(VaccAppVievModel.Fødselsdato), VaccAppVievModel.TelefonNr);
                VaccAppVievModel.Singleton.TilføjBarn(tempbarn);
                VaccAppVievModel.Singleton.hent();
            }
            catch (Exception x)
            {

                var dialog = new MessageDialog(x.Message);
                await dialog.ShowAsync();
            }

        }

        public void SletBarn()
        {
            VaccAppVievModel.Singleton.FjernBarn(VaccAppVievModel.SelectedBarn);
            
        }



        public async void HentVacciner()
        {
          await Model.Singleton.Instance.HentVacSkema();
        }

        



    }
}

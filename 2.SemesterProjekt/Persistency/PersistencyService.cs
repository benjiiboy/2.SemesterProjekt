using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using _2.SemesterProjekt.Model;

namespace _2.SemesterProjekt.Persistency
{
    public class PersistencyService
    {
        const string serverUrl = "http://vacws.azurewebsites.net/";
        const string apibørn = "api/barn/";


        public static void PostBarn(Barn PostBarn)
        {

            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = Client.PostAsJsonAsync(apibørn, PostBarn).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // ide til post fra daniel
                        //TODO: kig om det virker
                        List<Skema> sList = Singleton.VaccAppSingletion.VaccineSkemaListe.ToList();
                        foreach (Skema s in sList)
                        {
                            DateTime injDate = PostBarn.Fødselsdato.AddMonths(s.Tid);
                            VacPlan vp = new VacPlan(injDate, false, PostBarn.Barn_Id, s.Vac_Id);
                            PostVacPlan(vp);
                        }


                        MessageDialog BarnAdded = new MessageDialog("Dit barn blev tilføjet");
                        BarnAdded.Commands.Add(new UICommand { Label = "Ok" });
                        BarnAdded.ShowAsync().AsTask();
                    }
                }
                catch (Exception e)
                {
                    MessageDialog BarnAdded = new MessageDialog("Fejl, barn blev ikke tilføjet" + e);
                    BarnAdded.Commands.Add(new UICommand { Label = "Ok" });
                    BarnAdded.ShowAsync().AsTask();
                }

            }
        }

        public static ObservableCollection<Barn> GetBarn()
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                Client.DefaultRequestHeaders.Clear();
                var response = Client.GetAsync(apibørn).Result;

                if (response.IsSuccessStatusCode)
                {
                    var BørnList = response.Content.ReadAsAsync<ObservableCollection<Barn>>().Result;

                    return BørnList;
                }

                return null;
            }
        }

        public static void DeleteBarn(Barn DeleteBarn)
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                Client.DefaultRequestHeaders.Clear();
                string urlString = apibørn + DeleteBarn.Barn_Id;
                try
                {
                    var response = Client.DeleteAsync(urlString).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageDialog BarnDeleted = new MessageDialog("Dit barn blev slettet");
                        BarnDeleted.Commands.Add(new UICommand { Label = "Ok" });
                        BarnDeleted.ShowAsync().AsTask();
                    }
                }
                catch (Exception e)
                {
                    MessageDialog Error = new MessageDialog("Fejl, barn blev ikke slettet" + e);
                    Error.Commands.Add(new UICommand { Label = "Ok" });
                    Error.ShowAsync().AsTask();
                }
            }
        }

        public static void PutBarn(Barn PutBarn)
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string urlString = apibørn + PutBarn.Barn_Id;

                try
                {
                    var response = Client.PutAsJsonAsync(urlString, PutBarn).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageDialog BarnUpdated = new MessageDialog("Barn opdateret");
                        BarnUpdated.Commands.Add(new UICommand { Label = "Ok" });
                        BarnUpdated.ShowAsync().AsTask();
                    }
                }
                catch (Exception e)
                {
                    MessageDialog Error = new MessageDialog("Fejl, barn blev IKKE opdateret" + e);
                    Error.Commands.Add(new UICommand { Label = "Ok" });
                    Error.ShowAsync().AsTask();
                }
            }
        }


        //mik VacPlan

        const string apiVacPlan = "api/VacPlan/";
        const string apiSkema = "api/skema/";

        public static async Task<ObservableCollection<VacSkemaBarnPlan>> GetVacPlanAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();



                HttpResponseMessage vacplanrespone = await client.GetAsync(apiVacPlan);
                HttpResponseMessage barnrespone = await client.GetAsync(apibørn);
                HttpResponseMessage skemarespone = await client.GetAsync(apiSkema);

                if (vacplanrespone.IsSuccessStatusCode && barnrespone.IsSuccessStatusCode && skemarespone.IsSuccessStatusCode)
                {
                    ObservableCollection<VacPlan> VacPlanListe = await vacplanrespone.Content.ReadAsAsync<ObservableCollection<VacPlan>>();

                    ObservableCollection<Barn> VacBarnListe = await barnrespone.Content.ReadAsAsync<ObservableCollection<Barn>>();

                    ObservableCollection<Skema> SkemaListe = await skemarespone.Content.ReadAsAsync<ObservableCollection<Skema>>();

                    ObservableCollection<VacSkemaBarnPlan> vacplanogbarnListe = new ObservableCollection<VacSkemaBarnPlan>();

                    var Vacplanogbarnjoin = from barn in VacBarnListe
                                            join plan in VacPlanListe on barn.Barn_Id equals plan.Barn_Id
                                            select new { barn.Fornavn, barn.Efternavn, barn.Fødselsdato, plan.VaccineTid, plan.TrueFalse };

                    foreach (var item in Vacplanogbarnjoin)
                    {

                        VacSkemaBarnPlan derpbarn = new VacSkemaBarnPlan(item.VaccineTid, item.Fornavn, item.Efternavn, item.Fødselsdato, item.TrueFalse);
                        vacplanogbarnListe.Add(derpbarn);
                    }

                    ObservableCollection<VacSkemaBarnPlan> VacPlanBarnSkemaListe = new ObservableCollection<VacSkemaBarnPlan>();

                    var Vacplanbarnskemajoin = from skema in SkemaListe
                                               join vacplanbarn in vacplanogbarnListe on skema.Vac_Id equals vacplanbarn.Vac_Id
                                               select new { skema.Tid, skema.VaccineNavn, vacplanbarn.VaccineTid, vacplanbarn.TrueFalse, vacplanbarn.Fornavn, vacplanbarn.Efternavn, vacplanbarn.Fødselsdato };

                    foreach (var item in Vacplanbarnskemajoin)
                    {
                        VacSkemaBarnPlan derpbarn = new VacSkemaBarnPlan(item.Tid, item.Fornavn, item.Efternavn, item.VaccineNavn, item.TrueFalse, item.Fødselsdato, item.VaccineTid);
                        VacPlanBarnSkemaListe.Add(derpbarn);
                    }



                    //evt query for spec'? -> return

                    return VacPlanBarnSkemaListe;
                }
                return null;
            }
        }

        /*Benjamin ide til post vacplan*/

        public static void PostVacPlan(VacPlan PostVacPlan)
        {

            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = Client.PostAsJsonAsync(apiVacPlan, PostVacPlan).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        //MessageDialog BarnAdded = new MessageDialog("Dit barn blev tilføjet");
                        //BarnAdded.Commands.Add(new UICommand { Label = "Ok" });
                        //BarnAdded.ShowAsync().AsTask();
                        // den skal da ikke gøre noget når den er succes, daa det ville poppe op hele tiden
                    }
                }
                catch (Exception e)
                {
                    MessageDialog BarnAdded = new MessageDialog("Fejl, barn blev ikke tilføjet til VacPlan" + e);
                    BarnAdded.Commands.Add(new UICommand { Label = "Ok" });
                    BarnAdded.ShowAsync().AsTask();
                }






            }
        }
    }
}
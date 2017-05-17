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

        public static async Task<ObservableCollection<PlanBarn>> GetVacPlanAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();



                HttpResponseMessage vacplanrespone = await client.GetAsync(apiVacPlan);
                HttpResponseMessage barnrespone = await client.GetAsync(apibørn);

                if (vacplanrespone.IsSuccessStatusCode && barnrespone.IsSuccessStatusCode)
                {
                    ObservableCollection<VacPlan> VacPlanListe = await vacplanrespone.Content.ReadAsAsync<ObservableCollection<VacPlan>>();

                    ObservableCollection<Barn> VacBarnListe = await barnrespone.Content.ReadAsAsync<ObservableCollection<Barn>>();

                    ObservableCollection<PlanBarn> derp = new ObservableCollection<PlanBarn>();

                    var listejoin = from barn in VacBarnListe
                                    join plan in VacPlanListe on barn.Barn_Id equals plan.Barn_Id
                                    select new { barn.Fornavn, barn.Efternavn, plan.VaccineNavn, plan.TrueFalse, plan.Tid };



                    foreach (var item in listejoin)
                    {
                        PlanBarn derpbarn = new PlanBarn(item.Fornavn, item.Efternavn, item.VaccineNavn, item.TrueFalse, item.Tid);

                        derp.Add(derpbarn);
                    }

                    //evt query for spec'? -> return

                    return derp;
                }
                return null;
            }
        }


    }
}
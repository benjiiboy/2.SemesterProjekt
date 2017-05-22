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
                    PostBarn = response.Content.ReadAsAsync<Barn>().Result;

                    if (response.IsSuccessStatusCode)
                    {

                        VacPlan vp = new VacPlan();

                        ObservableCollection<Vaccine> sList = GetVaccine();
                        foreach (Vaccine s in sList)
                        {
                            DateTime injDate = PostBarn.Fødselsdato.AddMonths(s.Tid);

                            vp.Plan_Id = 0;
                            vp.Barn_Id = PostBarn.Barn_Id;
                            vp.TrueFalse = false;
                            vp.VaccineTid = injDate;
                            vp.Vac_Id = s.Vac_Id;

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
        const string apiVaccine = "api/Vaccine/";

        public static async Task<ObservableCollection<VacSkemaBarnPlan>> GetVacPlanAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();



                HttpResponseMessage vacplanrespone = await client.GetAsync(apiVacPlan);
                HttpResponseMessage barnrespone = await client.GetAsync(apibørn);
                HttpResponseMessage Vaccinerespone = await client.GetAsync(apiVaccine);

                if (vacplanrespone.IsSuccessStatusCode && barnrespone.IsSuccessStatusCode && Vaccinerespone.IsSuccessStatusCode)
                {
                    ObservableCollection<VacPlan> VacPlanListe = await vacplanrespone.Content.ReadAsAsync<ObservableCollection<VacPlan>>();

                    ObservableCollection<Barn> VacBarnListe = await barnrespone.Content.ReadAsAsync<ObservableCollection<Barn>>();

                    ObservableCollection<Vaccine> VaccineListe = await Vaccinerespone.Content.ReadAsAsync<ObservableCollection<Vaccine>>();

                    ObservableCollection<VacSkemaBarnPlan> vacplanogbarnListe = new ObservableCollection<VacSkemaBarnPlan>();

                    var Vacplanogbarnjoin = from barn in VacBarnListe
                                            join plan in VacPlanListe on barn.Barn_Id equals plan.Barn_Id
                                            select new {plan.Vac_Id, barn.Barn_Id, barn.Fornavn, barn.Efternavn, barn.Fødselsdato, plan.VaccineTid, plan.TrueFalse };

                    foreach (var item in Vacplanogbarnjoin)
                    {

                        VacSkemaBarnPlan derpbarn1 = new VacSkemaBarnPlan(item.Vac_Id, item.Barn_Id, item.VaccineTid, item.Fornavn, item.Efternavn, item.Fødselsdato, item.TrueFalse);
                        vacplanogbarnListe.Add(derpbarn1);
                    }

                    ObservableCollection<VacSkemaBarnPlan> VacPlanBarnVaccineListe = new ObservableCollection<VacSkemaBarnPlan>();

                    var VacplanbarnVaccinejoin = from Vaccine in VaccineListe
                                               join vacplanbarn in vacplanogbarnListe on Vaccine.Vac_Id equals vacplanbarn.Vac_Id
                                               select new {Vaccine.Vac_Id, vacplanbarn.Barn_Id, Vaccine.VaccineNavn, vacplanbarn.VaccineTid, vacplanbarn.TrueFalse, vacplanbarn.Fornavn, vacplanbarn.Efternavn};

                    foreach (var item in VacplanbarnVaccinejoin)
                    {
                        VacSkemaBarnPlan derpbarn = new VacSkemaBarnPlan(item.Vac_Id, item.Barn_Id, item.Fornavn, item.Efternavn, item.VaccineNavn, item.TrueFalse, item.VaccineTid);
                        VacPlanBarnVaccineListe.Add(derpbarn);
                    }



                    //evt query for spec'? -> return

                    return VacPlanBarnVaccineListe;
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
                        //hvis  response code er positiv ville man få 12 beskeder, om at det gik godt, derfor er der ingen
                    }
                }
                catch (Exception e)
                {
                    MessageDialog VacplanAdded = new MessageDialog("Fejl, Vacciner blev ikke oprettet til dit barn" + e);
                    VacplanAdded.Commands.Add(new UICommand { Label = "Ok" });
                    VacplanAdded.ShowAsync().AsTask();
                }

            }
        }



       public static ObservableCollection<Vaccine> GetVaccine()
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                Client.DefaultRequestHeaders.Clear();
                var response = Client.GetAsync(apiVaccine).Result;

                if (response.IsSuccessStatusCode)
                {
                    var VaccineList = response.Content.ReadAsAsync<ObservableCollection<Vaccine>>().Result;

                    return VaccineList;
                }

                return null;
            }
        }

        public static ObservableCollection<VacPlan> GetVacPlan()
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                Client.DefaultRequestHeaders.Clear();
                var response = Client.GetAsync(apiVacPlan).Result;

                if (response.IsSuccessStatusCode)
                {
                    var VacPlanList = response.Content.ReadAsAsync<ObservableCollection<VacPlan>>().Result;

                    return VacPlanList;
                }

                return null;
            }
        }

        //mik hent vaccine
        public static async Task<ObservableCollection<Vaccine>> GetVaccineAsync()
        {
            using (var Client = new HttpClient())
            {
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Client.BaseAddress = new Uri(serverUrl);
                Client.DefaultRequestHeaders.Clear();
                try
                {
                    HttpResponseMessage GetVaccineResponse = await Client.GetAsync(apiVaccine);

                    if (GetVaccineResponse.IsSuccessStatusCode)
                    {
                        var TempVaccineCollection = await GetVaccineResponse.Content.ReadAsAsync<ObservableCollection<Vaccine>>();
                        return TempVaccineCollection;
                    }
                }
                catch (Exception)
                {
                    //skal ændres!
                    MessageDialog VacplanAdded = new MessageDialog("Fejl, Vacciner blev ikke oprettet til dit barn");
                    VacplanAdded.Commands.Add(new UICommand { Label = "Ok" });
                    await VacplanAdded.ShowAsync().AsTask();
                }
                return null;
            }
        }
        




    }
    }

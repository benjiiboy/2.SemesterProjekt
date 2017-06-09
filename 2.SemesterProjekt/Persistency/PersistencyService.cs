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
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace _2.SemesterProjekt.Persistency
{
    public class PersistencyService
    {
        const string serverUrl = "http://vacws.azurewebsites.net/";
        const string apibørn = "api/barn/";
        const string apiVacPlan = "api/VacPlan/";
        const string apiVaccine = "api/Vaccine/";


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
                            //postbarn.fødselsdato.addmoths(s.tid;)
                            DateTime injDate = PostBarn.Fødselsdato.AddSeconds(s.Tid);

                            vp.Plan_Id = 0;
                            vp.Barn_Id = PostBarn.Barn_Id;
                            vp.TrueFalse = false;
                            vp.VaccineTid = injDate;
                            vp.Vac_Id = s.Vac_Id;


                            PostVacPlan(vp);

                            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
                            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

                            IXmlNode toasttextelements = toastXml.GetElementsByTagName("text").FirstOrDefault();
                            toasttextelements.AppendChild(toastXml.CreateTextNode($"{PostBarn.Fornavn} {PostBarn.Efternavn} skal have vaccine nr {s.Vac_Id} {s.VaccineNavn} d. {vp.VaccineTid.ToString("dd-MM-yyyy")} "));

                            /*Ændre datetime.now til vp.vaccinetid.adddays(-14)*/
                            DateTime dueTime = DateTime.Now.AddSeconds(5);

                            ScheduledToastNotification scheduledToast = new ScheduledToastNotification(toastXml, dueTime);

                            ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduledToast);

                        }



                        ShowMessage("Dit barn blev tilføjet");
                    }
                }
                catch (Exception e)
                {
                    ShowMessage("Fejl, barn blev ikke tilføjet" + e.Message);
                }

            }
        }






        public static ObservableCollection<Barn> GetBarn()
        {
            try
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

                }
            }
            catch (Exception e)
            {
                ShowMessage($"Connection error: {e.Message}");
            }
            return new ObservableCollection<Barn>();


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
                        ShowMessage("Dit barn blev slettet");
                    }
                }
                catch (Exception e)
                {
                    ShowMessage("Fejl, barn blev ikke slettet" + e);
                }
            }
        }

        public static async Task<ObservableCollection<VacSkemaBarnPlan>> GetVacSkemaBarnPlanAsync()
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
                                            select new { plan.Vac_Id, barn.Barn_Id, barn.Fornavn, barn.Efternavn, barn.Fødselsdato, plan.VaccineTid, plan.TrueFalse };

                    foreach (var item in Vacplanogbarnjoin)
                    {
                        VacSkemaBarnPlan foreachliste = new VacSkemaBarnPlan(item.Vac_Id, item.Barn_Id, item.VaccineTid, item.Fornavn, item.Efternavn, item.Fødselsdato, item.TrueFalse);
                        vacplanogbarnListe.Add(foreachliste);
                    }

                    ObservableCollection<VacSkemaBarnPlan> VacPlanBarnVaccineListe = new ObservableCollection<VacSkemaBarnPlan>();

                    var VacplanbarnVaccinejoin = from Vaccine in VaccineListe
                                                 join vacplanbarn in vacplanogbarnListe on Vaccine.Vac_Id equals vacplanbarn.Vac_Id
                                                 select new { Vaccine.Vac_Id, vacplanbarn.Barn_Id, Vaccine.VaccineNavn, vacplanbarn.VaccineTid, vacplanbarn.TrueFalse, vacplanbarn.Fornavn, vacplanbarn.Efternavn };

                    foreach (var item in VacplanbarnVaccinejoin)
                    {
                        VacSkemaBarnPlan foreachliste = new VacSkemaBarnPlan(item.Vac_Id, item.Barn_Id, item.Fornavn, item.Efternavn, item.VaccineNavn, item.TrueFalse, item.VaccineTid);
                        VacPlanBarnVaccineListe.Add(foreachliste);
                    }

                    return VacPlanBarnVaccineListe;
                }
                return null;
            }
        }
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

                }
                catch (Exception e)
                {
                    ShowMessage("Fejl, Vacciner blev ikke oprettet til dit barn" + e);
                }

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
        public static async Task<List<Vaccine>> GetSorteredeVaccineAsync()  
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
                        var TempVaccineCollection = await GetVaccineResponse.Content.ReadAsAsync<List<Vaccine>>();
                        var sortedelist = TempVaccineCollection.GroupBy(x => x.VaccineNavn).Select(g => g.First()).ToList();
                        return sortedelist;
                    }
                }
                catch (Exception e)
                {
                    ShowMessage($"Fejl, Vaccineinfo blev ikke opdateret: {e.Message}");
                }
                return null;
            }
        }
        public static async Task<List<Vaccine>> GetVaccineAsync()
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
                        var TempVaccineCollection = await GetVaccineResponse.Content.ReadAsAsync<List<Vaccine>>();
                        return TempVaccineCollection;
                    }
                }
                catch (Exception e)
                {
                    ShowMessage($"Fejl, Vaccinekort blev ikke opdateret: {e.Message}");
                }
                return null;
            }
        }

        public static async void ShowMessage(string content)
        {
            MessageDialog messageDialog = new MessageDialog(content);
            await messageDialog.ShowAsync();
        }

    }
}

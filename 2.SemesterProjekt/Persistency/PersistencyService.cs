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
        const string serverUrl = "http://vaccappws.azurewebsites.net/";
        

        public static void PostBarn(Barn PostBarn)
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = Client.PostAsJsonAsync("api/børn/", PostBarn).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        MessageDialog BarnAdded = new MessageDialog("Dit barn blev tilføjet");
                        BarnAdded.Commands.Add(new UICommand { Label = "Ok" });
                        BarnAdded.ShowAsync().AsTask();
                    }
                }
                catch (Exception e)
                {
                    MessageDialog BarnAdded = new MessageDialog("Dit barn blev ikke tilføjet");
                    BarnAdded.Commands.Add(new UICommand { Label = "Ok" });
                    BarnAdded.ShowAsync().AsTask();
                    throw;
                }

            }
        }

        public static ObservableCollection<Barn> GetBarn()
        {
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri(serverUrl);
                var response = Client.GetAsync("api/børn/").Result;

                if (response.IsSuccessStatusCode)
                {
                    var BørnList = response.Content.ReadAsAsync
                }


            }
        }
    }
}

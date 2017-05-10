using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Popups;
using System.Diagnostics;
using Windows.UI.Notifications;

namespace _2.SemesterProjekt.Handler
{
   public class Notifikation
    {
                





        public static async void ShowMessage(string content)
        {
            MessageDialog messageDialog = new MessageDialog(content);
            await messageDialog.ShowAsync();
        }

        //TODO: push notifikation skal laves her!!!!



    }
}

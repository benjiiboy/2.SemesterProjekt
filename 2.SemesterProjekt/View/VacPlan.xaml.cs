using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace _2.SemesterProjekt.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VacPlan : Page
    {
        public VacPlan()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            main();
        }


        public void main()
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            IXmlNode toasttextelements = toastXml.GetElementsByTagName("text").FirstOrDefault();
            toasttextelements.AppendChild(toastXml.CreateTextNode("Husk vaccine!"));

            var dueTimeSeconds = 3;
            DateTime dueTime = DateTime.Now.AddSeconds(dueTimeSeconds);

            ScheduledToastNotification scheduledToast = new ScheduledToastNotification(toastXml, dueTime);

            ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduledToast);


        }



    }
}

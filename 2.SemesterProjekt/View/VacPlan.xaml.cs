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
            ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText01;
            XmlDocument toastxml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            IXmlNode toasttextelements = toastxml.GetElementsByTagName("text").FirstOrDefault();
            toasttextelements.AppendChild(toastxml.CreateTextNode("Husk vaccine!"));

            ToastNotification toast1 = new ToastNotification(toastxml);
            ToastNotificationManager.CreateToastNotifier().Show(toast1);
        }
    }
}

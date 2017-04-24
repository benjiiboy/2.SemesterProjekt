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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _2.SemesterProjekt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MainPageFrame.Navigate(typeof(View.Menu));
        }


        private void FrameGåTilbage()
        {
            if (MainPageFrame.CanGoBack)
            {
                MainPageFrame.GoBack();
            }
        }

        private void FrameGåFrem()
        {
            if (MainPageFrame.CanGoForward)
            {
                MainPageFrame.GoForward();
            }
        }

        private void MainPageHjem_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(typeof(View.Menu));
        }
    }
}

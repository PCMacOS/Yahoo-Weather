using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI; //Title Bar Color
using Windows.UI.ViewManagement;//Title Bar Color

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Yahoo_Weather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            WebView1.Navigate(new Uri("https://www.yahoo.com/news/weather"));
            //DeleteDivs();

        }

        /*public async void DeleteDivs()
        {
            var script = new string[]
            {
                "var Bottom = document.getElementById(\"YDC - Bottom\") cells.parentNode.removeChild(Bottom); var add = document.getElementById(\"Col2 - 1 - WeatherVideoList - Proxy\") cells.parentNode.removeChild(add); var Side = document.getElementById(\"YDC - Side\") cells.parentNode.removeChild(Side); var UH = document.getElementById(\"YDC - UH\") cells.parentNode.removeChild(UH); var Side = document.getElementById(\"YDC - Side\") cells.parentNode.removeChild(Side);"
            };
            await WebView1.InvokeScriptAsync("eval", script);
        }*/

        private static void SetTitleBarBackground()
        {
            // Get the instance of the Title Bar
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;

            // Set the color of the Title Bar content
            titleBar.BackgroundColor = Color.FromArgb(1, 45, 17, 82);
            titleBar.ForegroundColor = Colors.AliceBlue;

            // Set the color of the Title Bar buttons
            titleBar.ButtonBackgroundColor = Color.FromArgb(1, 45, 17, 82);
            titleBar.ButtonForegroundColor = Colors.AliceBlue;
            titleBar.ButtonHoverBackgroundColor = Colors.BlueViolet;
            titleBar.ButtonHoverForegroundColor = Colors.AliceBlue;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SetTitleBarBackground();
            var currentView = SystemNavigationManager.GetForCurrentView();
            //1. Make the button Visible
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible; //Show Button
            //2. Detect button presses
            currentView.BackRequested += backButton_Tapped;

            
        }

        private void backButton_Tapped(object sender, BackRequestedEventArgs e)
        {
            if(WebView1.CanGoBack) WebView1.GoBack();
        }
    }
}

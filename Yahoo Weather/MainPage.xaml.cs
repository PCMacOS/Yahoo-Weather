using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
            
            DeleteTags("https://www.yahoo.com/news/weather");

        }

        async Task DeleteTags(string url)
        {
            //Create an HTTP client object
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            
            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";
            try
            {

                //Send the GET request
                httpResponse = await httpClient.GetAsync(new Uri(url));
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                
                //Hidde some tags
                httpResponseBody = Regex.Replace(httpResponseBody, "div id=\"YDC-UH\"", "div id=\"YDC-UH\" hidden");
                httpResponseBody = Regex.Replace(httpResponseBody, "<div id=\"YDC-Side-Stack\"", "<div id=\"YDC-Side-Stack\" hidden");
                httpResponseBody = Regex.Replace(httpResponseBody, "<div class=\"c", "<div hidden class=\"c");
                httpResponseBody = Regex.Replace(httpResponseBody, "<div id=\"YDC-Bottom\" ", "<div id=\"YDC-Bottom\" hidden ");
                //httpResponseBody = Regex.Replace(httpResponseBody, "<button class=\"D.ib.", "<button hidden class=\"D.ib.");

            }
            catch (Exception ex)
            {
                httpResponseBody =
                    "<!DOCTYPE html> <html> <head> <link href=\"https://fonts.googleapis.com/css?family=Roboto\" rel=\"stylesheet\"> </head> <body style=\"background-color:BlueViolet;\"> <div style=\"width: 300px; height: 300px; position: absolute; left: 50%; top: 50%; margin-left: -150px; margin-top: -150px;\"><center><font color=\"white\"><h1 style=\"font-family: 'Roboto', sans-serif;\">" +
                    "</ h1 ></ font ></ center ></ div > </ body > </ html > " + "Error: " + ex.HResult.ToString("X") +
                    " Message: " + ex.Message.TrimEnd() + ".";

            }
            WebView1.NavigateToString(httpResponseBody);
        }

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
            if (WebView1.CanGoBack) currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible; //Show Button
            //2. Detect button presses
            currentView.BackRequested += backButton_Tapped;

            
        }

        private void backButton_Tapped(object sender, BackRequestedEventArgs e)
        {
            if (WebView1.CanGoBack) WebView1.GoBack();
        }

        private void ShowWebView(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            LoadingText.Visibility = Visibility.Collapsed;
            WebView1.Visibility = Visibility.Visible;
        }
    }
}

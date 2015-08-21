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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.Phone.UI.Input;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Mobile_Shaver
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static bool starting = true;

        public enum PlayerState { Playing, Stopped, Paused };
        public bool inverted;

        private PlayerState state = PlayerState.Stopped;
        StatusBar statusBar;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            statusBar = StatusBar.GetForCurrentView();
            inverted = false;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.

            await statusBar.HideAsync();

            HardwareButtons.BackPressed += exitApp;
        }

        private void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (state == PlayerState.Stopped)
            {
                state = PlayerState.Playing;
                player.Play();
            }
            else if (state == PlayerState.Playing)
            {
                state = PlayerState.Paused;
                player.Pause();
            }
            else if (state == PlayerState.Paused)
            {
                state = PlayerState.Playing;
                player.Play();
            }

        }

        private void player_MediaEnded(object sender, RoutedEventArgs e)
        {
            player.Play();
        }

        private void Flip_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage image = new BitmapImage();

            if (inverted)
            {
                image.UriSource = new Uri("ms-appx:///assets/Background.png", UriKind.RelativeOrAbsolute);
                background.Source = image;
                inverted = false;
                hot_zone.Margin = new Thickness(0, 80, 0, 0);
            }
            else
            {
                image.UriSource = new Uri("ms-appx:///assets/Inverted.png", UriKind.RelativeOrAbsolute);
                background.Source = image;
                inverted = true;
                hot_zone.Margin = new Thickness(0, 0, 0, 90);
            }
        }

        private void exitApp(object sender, BackPressedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void visibility_Toggled(object sender, RoutedEventArgs e)
        {
            if (starting == true)
            {
                starting = false;
            }
            else if ((sender as ToggleSwitch).IsOn)
            {
                ShowImage.Begin();
            }
            else
            {
                HideImage.Begin();
            }









            /* 
             * 无动画版本
             */
            //else if ((sender as ToggleSwitch).IsOn)
            //{
            //    background.Opacity = 1;
            //}
            //else
            //{
            //    background.Opacity = 0;
            //}

        }
    }
}
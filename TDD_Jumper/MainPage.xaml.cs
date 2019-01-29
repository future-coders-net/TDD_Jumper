using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace TDD_Jumper
{
    public sealed partial class MainPage : Page
    {
        BitmapImage[] images = new BitmapImage[8];
        DispatcherTimer timer;
        int counter = 0;

        public MainPage()
        {
            this.InitializeComponent();

            for (int i = 0; i < 8; i++)
            {
                images[i] = new BitmapImage(
                    new Uri("ms-appx:///Images/stickman" + i + ".png"));
            }
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(30);
            timer.Tick += Tick;
            timer.Start();
        }

        private void Tick(object sender, object e)
        {
            counter = (counter + 1) % 8;
            Stickman.Source = images[counter];
        }
    }
}

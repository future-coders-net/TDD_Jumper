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
        int speed = 0;
        int boxX = 600;
        int stickY = 200;

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

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender,
            Windows.UI.Core.KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.Space &&
                stickY == 200)
            {
                speed = -15;
            }
        }

        private void Tick(object sender, object e)
        {
            counter = (counter + 1) % 8;
            Stickman.Source = images[counter];

            boxX = (boxX > -100) ? boxX - 10 : 600;
            Box.SetValue(Canvas.TopProperty, 300);
            Box.SetValue(Canvas.LeftProperty, boxX);

            speed += 1;
            stickY += speed;
            if (stickY > 200)
            {
                stickY = 200;
                speed = 0;
            }
            Stickman.SetValue(Canvas.TopProperty, stickY);
        }
    }
}

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Foundation;

namespace TDD_Jumper
{
    public sealed partial class MainPage : Page
    {
        BitmapImage[] images = new BitmapImage[8];
        DispatcherTimer timer;
        int counter = 0;
        int speed = 0;
        Rect stickR, boxR;

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

            DataContext = this;
            stickR = new Rect(20, 200, 100, 150);
            boxR = new Rect(600, 300, 50, 200);
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender,
            Windows.UI.Core.KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.Space &&
                stickR.Y == 200)
            {
                speed = -17;
            }
        }

        private void Tick(object sender, object e)
        {
            counter = (counter + 1) % 8;
            Stickman.Source = images[counter];

            speed += 1;
            stickR.Y = Math.Min(200, stickR.Y + speed);
            Stickman.SetValue(Canvas.TopProperty, stickR.Y);

            boxR.X = (boxR.X > -100) ? boxR.X - 10 : 600;
            Box.SetValue(Canvas.LeftProperty, boxR.X);

            if (RectHelper.Intersect(boxR, stickR) != Rect.Empty)
            {
                GameOver.Visibility = Visibility.Visible;
                timer.Stop();
            }
        }
    }
}

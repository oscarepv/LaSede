using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaSede
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new vistaLogin())
            // MainPage = new NavigationPage(new vistaCanchas ())
            { BarBackgroundColor = Color.FromRgb(38, 173, 134), BarTextColor = Color.White }; ;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

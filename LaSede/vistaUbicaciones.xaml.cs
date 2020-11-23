using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace LaSede
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaUbicaciones : ContentPage
    {
        public vistaUbicaciones()
        {
            InitializeComponent();
        }

        private async void btnAbrir_Clicked(object sender, EventArgs e)
        {
            if (!double.TryParse(txtLatitud.Text, out double lat)) {
                return;
            }
            if (!double.TryParse(txtlongitud.Text, out double lng))
            {
                return;
            }

            await Map.OpenAsync(lat, lng, new MapLaunchOptions
            {
                Name = txtNombre.Text,
                NavigationMode = NavigationMode.None
            });
        }
    }
}
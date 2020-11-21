using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LaSede.Session;

namespace LaSede
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaMenu : ContentPage
    {
        private string idUsuario;
        public vistaMenu()
        {
            InitializeComponent();
            idUsuario = UserSettings.userId;
            lblNombre.Text = "Bienvenid@: "+ UserSettings.userName;
        }

        private void btnVistaPerfil_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new vistaPerfil());
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new vistaLogin());
            UserSettings.ClearAllData();
        }

        private void btnVistaAlquiler_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new vistaTipos());
        }
    }
}
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
    public partial class vistaMenuAdministrador : ContentPage
    {
        private string idUsuario;
        public vistaMenuAdministrador()
        {
            InitializeComponent();
            idUsuario = UserSettings.userId;
            lblNombre.Text = "Bienvenid@: " + UserSettings.userName;
        }

        private void btnVistaHorarios_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new horarios());
        }

        private void btnVistaCanchas_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new vistaCanchas("C"));
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new vistaLogin());
            UserSettings.ClearAllData();
        }

        private void btnVistaHorarioCanchas_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new vistaCanchas("H"));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LaSede.Session;
using LaSede.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace LaSede
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaLogin : ContentPage
    {
        private const string Url = "http://dmoviles-proyecto.atwebpages.com/usuarioPost.php";
        HttpClient cliente = new HttpClient();

        public vistaLogin()
        {
            InitializeComponent();
        }

        private async void tapRegistro_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new vistaRegistro());
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                string correo = txtEmail.Text;
                string passwd = txtPasswd.Text;

                var content = await cliente.GetStringAsync(Url + "?correo=" + correo);
                if (content == "false")
                {
                    await DisplayAlert("Alerta", "Usuario no encontrado", "Ok");
                }
                else
                {
                    Models.Usuario usuario = JsonConvert.DeserializeObject<Models.Usuario>(content);
                    if (usuario.correo == txtEmail.Text && usuario.password == txtPasswd.Text)
                    {
                        UserSettings.userId = usuario.id_Usuario;
                        UserSettings.userName = usuario.nombres + " " + usuario.apellidos;
                        await DisplayAlert("Alerta", "Bienvenido: " + usuario.nombres, "Ok");
                        await Navigation.PushAsync(new vistaMenu());
                    }
                    else
                    {
                        await DisplayAlert("Alerta", "La contraseña es incorrecta", "Ok");
                    }
                }

            }
            catch (Exception ex)
            {

                await DisplayAlert("Alerta", ex.Message, "Ok");
            }
            

        }
    }
}
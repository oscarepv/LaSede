using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LaSede.Models;
using System.Net.Http;

namespace LaSede
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaRegistro : ContentPage
    {
        private const string Url = "http://dmoviles-proyecto.atwebpages.com/usuarioPost.php";
        HttpClient cliente = new HttpClient();
        public vistaRegistro()
        {
            InitializeComponent();
        }

        private async void btnRegistro_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (txtPasswd.Text == txtRePasswd.Text)
                {

                    var parametros = new Dictionary<string, string>();
                    parametros.Add("nombres", txtNombre.Text);
                    parametros.Add("apellidos", txtApellidos.Text);
                    parametros.Add("correo", txtEmail.Text);
                    parametros.Add("password", txtPasswd.Text);
                    parametros.Add("estado", "1");
                    parametros.Add("tipo_usuario", "1");

                    var req = new HttpRequestMessage(HttpMethod.Post, Url) { Content = new FormUrlEncodedContent(parametros) };
                    var res = await cliente.SendAsync(req);
                    await DisplayAlert("Alerta", "Usuario registrado correctamente", "Ok");
                    await Navigation.PushAsync(new vistaLogin());
                }
                else
                {
                    await DisplayAlert("Alerta", "Sus contraseñas no coiciden", "Ok");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "Error: " + ex.Message, "Ok");
            }
        }
    }
}
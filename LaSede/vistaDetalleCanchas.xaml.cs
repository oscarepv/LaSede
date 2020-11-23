using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace LaSede
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaDetalleCanchas : ContentPage
    {
        Models.Canchas cancha;
        private const string Url = "http://192.168.1.3/laSedeWebService/canchaPost.php";
        HttpClient cliente = new HttpClient();
        public vistaDetalleCanchas(int idCancha)
        {
            InitializeComponent();
            cancha = new Models.Canchas();
            cancha.id = idCancha;
        }
        protected async override void OnAppearing()
        {
            if (cancha.id != -1)
            {
                //await DisplayAlert("Alerta", horario.id.ToString(), "Ok");
                var content = await cliente.GetStringAsync(Url + "?id_cancha=" + cancha.id);
                //System.Console.WriteLine("Estados Horas: " + content.ToString());
                Models.Canchas c = JsonConvert.DeserializeObject<Models.Canchas>(content);
                txtNombre.Text = c.nombre;
                txtValor.Text = c.valor.ToString();
                btnGuardar.IsVisible = false;
            }
            else
            {
                btnEliminar.IsVisible = false;
                btnModificar.IsVisible = false;
            }


        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                cancha.estado = 0;

                if (cancha != null)
                {

                    var parametros = new Dictionary<string, string>();
                    parametros.Add("nombre", txtNombre.Text);
                    parametros.Add("valor", txtValor.Text);
                    parametros.Add("estado", cancha.estado.ToString());
                    parametros.Add("id_tipo_cancha", "1");

                    var req = new HttpRequestMessage(HttpMethod.Post, Url) { Content = new FormUrlEncodedContent(parametros) };
                    var res = await cliente.SendAsync(req);

                    await DisplayAlert("Alerta", "Cancha registrado correctamente", "Ok");
                    await Navigation.PushAsync(new vistaCanchas("C"));
                }
                else
                {
                    await DisplayAlert("Alerta", "Prueba", "Ok");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "Error: " + ex.Message, "Ok");
            }
        }

        private async void btnModificar_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    var parametros = new System.Collections.Specialized.NameValueCollection();
                    parametros.Add("id_cancha", cancha.id.ToString());
                    parametros.Add("nombre", txtNombre.Text);
                    parametros.Add("valor", txtValor.Text);
                    parametros.Add("estado", cancha.estado.ToString());
                    parametros.Add("id_tipo_cancha", "1");
                    client.UploadValues(Url, "PUT", client.QueryString = parametros);
                    await DisplayAlert("Alerta", "Modificado correctamente", "Ok");
                    await Navigation.PushAsync(new vistaCanchas("C"));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "Error: " + ex.Message, "ok");
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    var parametros = new System.Collections.Specialized.NameValueCollection();
                    parametros.Add("id_horario", cancha.id.ToString());
                    client.UploadValues(Url, "DELETE", client.QueryString = parametros);
                    await DisplayAlert("Alerta", "Eliminado correctamente", "Ok");
                    await Navigation.PushAsync(new vistaCanchas("C"));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "Error: " + ex.Message, "ok");
            }
        }
    }
}
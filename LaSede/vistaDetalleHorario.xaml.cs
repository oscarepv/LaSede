using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace LaSede
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaDetalleHorario : ContentPage
    {
        Models.Horario horario;
        private const string Url = "http://192.168.1.3/laSedeWebService/horarioPost.php";
        HttpClient cliente = new HttpClient();
       
        public vistaDetalleHorario(int id)
        {
            InitializeComponent();
            horario = new Models.Horario();
            horario.id = id;
        }
        protected async override void OnAppearing()
        {
            if (horario.id != -1) {
                //await DisplayAlert("Alerta", horario.id.ToString(), "Ok");
                var content = await cliente.GetStringAsync(Url + "?id_horario=" + horario.id);
                //System.Console.WriteLine("Estados Horas: " + content.ToString());
                Models.Horario h = JsonConvert.DeserializeObject<Models.Horario>(content);
                horaInicio.Time = h.horaInicio.TimeOfDay;
                horaFin.Time = h.horaFin.TimeOfDay;

                btnGuardar.IsVisible = false;
            } else
            {
                btnEliminar.IsVisible = false;
                btnModificar.IsVisible = false;
            }
            
            
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {

            try
            {
                horario.estado = 0;
                System.Console.WriteLine("hora: " + horaInicio.Time.ToString(@"hh\:mm"));

                if (horaInicio != null)
                {

                    var parametros = new Dictionary<string, string>();
                    parametros.Add("hora_inicio", horaInicio.Time.ToString(@"hh\:mm"));
                    parametros.Add("hora_fin", horaFin.Time.ToString(@"hh\:mm"));
                    parametros.Add("estado", horario.estado.ToString());
     
                    var req = new HttpRequestMessage(HttpMethod.Post, Url) { Content = new FormUrlEncodedContent(parametros) };
                    var res = await cliente.SendAsync(req);
                    
                    await DisplayAlert("Alerta", "Hora registrado correctamente", "Ok");
                    await Navigation.PushAsync(new horarios());
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
                    parametros.Add("id_horario", horario.id.ToString());
                    parametros.Add("hora_inicio", horario.horaInicio.ToString(@"hh\:mm"));
                    parametros.Add("hora_fin", horario.horaInicio.ToString(@"hh\:mm"));
                    parametros.Add("estado", horario.estado.ToString());
                    client.UploadValues(Url, "PUT", client.QueryString = parametros);
                    await DisplayAlert("Alerta", "Modificado correctamente", "Ok");
                    await Navigation.PushAsync(new horarios());
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
                    parametros.Add("id_horario", horario.id.ToString());
                    client.UploadValues(Url, "DELETE", client.QueryString = parametros);
                    await DisplayAlert("Alerta", "Eliminado correctamente", "Ok");
                    await Navigation.PushAsync(new horarios());
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "Error: " + ex.Message, "ok");
            }
        }
    }
}
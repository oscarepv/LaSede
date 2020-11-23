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
using LaSede.Session;

namespace LaSede
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaDetalleReservas : ContentPage
    {
        private Models.Reservas reserva;
        private const string Url = "http://192.168.1.3/laSedeWebService/reservaPost.php";
        private const string UrlCanchaHora = "http://192.168.1.3/laSedeWebService/canchaHoraPost.php";
        HttpClient cliente = new HttpClient();
        public vistaDetalleReservas(int idCanchaHora)
        {
            InitializeComponent();
            reserva = new Models.Reservas();
            reserva.idCanchaHora = idCanchaHora;
            reserva.idUsuario = int.Parse( UserSettings.userId);
            
        }

        protected async override void OnAppearing()
        {
            //await DisplayAlert("Alerta", reserva.idCanchaHora.ToString(), "Ok");

            var content = await cliente.GetStringAsync(UrlCanchaHora + "?id_cancha_r=" + reserva.idCanchaHora);
            //System.Console.WriteLine("Estados Horas: " + content.ToString());
            Models.CanchasHoras h = JsonConvert.DeserializeObject<Models.CanchasHoras>(content);
            lblCancha.Text = "Cancha: " + h.cancha;
            lblHoraI.Text = "Hora inicio: " + h.horaInicio;
            lblHoraF.Text = "Hora Fin: " + h.horaFin;
        }


            private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                reserva.estado = 0;
                if (txtObservacion.Text == null || string.IsNullOrEmpty(txtObservacion.Text))
                {
                    await DisplayAlert("Alerta", "Observación es necesaria", "Ok");
                    return;

                }
                if (reserva != null)
                {

                    var parametros = new Dictionary<string, string>();
                    string fecha = fechaReserva.Date.ToString("yyyy-MM-dd");
                    System.Console.WriteLine("fecha : " + fecha);
                    parametros.Add("fecha", fecha);
                    parametros.Add("observacion", txtObservacion.Text);
                    parametros.Add("id_usuario", reserva.idUsuario.ToString());
                    parametros.Add("id_cancha_hora", reserva.idCanchaHora.ToString());
                    parametros.Add("estado", reserva.estado.ToString());

                    var req = new HttpRequestMessage(HttpMethod.Post, Url) { Content = new FormUrlEncodedContent(parametros) };
                    var res = await cliente.SendAsync(req);

                    await DisplayAlert("Alerta", "Reserva registrado con éxito", "Ok");
                    await Navigation.PushAsync(new vistaCanchas("R"));
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
    }
}
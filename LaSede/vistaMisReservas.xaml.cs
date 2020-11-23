using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Net.Http;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using LaSede.Session;
using System.Net;

namespace LaSede
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaMisReservas : ContentPage
    {
        private string idUser;
        private string tipoUsuario = "1";
        private const string Url = "http://192.168.1.3/laSedeWebService/reservaPost.php";
        private readonly HttpClient reserva = new HttpClient();
        private ObservableCollection<Models.Reservas> _post;
        public vistaMisReservas()
        {
            InitializeComponent();
            idUser = UserSettings.userId;
            tipoUsuario = UserSettings.tiposuario;

        }

        protected override void OnAppearing()
        {
            buscarReservas();
        }

        private async void buscarReservas()
        {
            var content = await reserva.GetStringAsync(Url + "?id_especial=" + idUser);
            List<Models.Reservas> posts = JsonConvert.DeserializeObject<List<Models.Reservas>>(content);
            _post = new ObservableCollection<Models.Reservas>(posts);

            listaReservas.ItemsSource = _post;
            /*posts.ForEach(r => {
                r.hora = traerHora(4).Result;
            });*/
        }

        private async void listaReservas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Models.Reservas)e.SelectedItem;
            var action = await DisplayAlert("Alerta", "¿Está seguro de cancelar reserva?", "Si", "No");
            if (action)
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        var parametros = new System.Collections.Specialized.NameValueCollection();
                        parametros.Add("id_reserva", (item.id.ToString()));
                        client.UploadValues(Url, "DELETE", client.QueryString = parametros);
                        await DisplayAlert("Alerta", "Eliminado correctamente", "Ok");
                        buscarReservas();
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alerta", "Error: " + ex.Message, "ok");
                }
                //await Navigation.PushAsync(new vistaDetalleCanchaHora(item.id));
            }
        }

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            if (tipoUsuario.Equals("1"))
            {
                await Navigation.PushAsync(new vistaMenu());
            }
            else
            {
                await Navigation.PushAsync(new vistaMenuAdministrador());
            }
        }
      
    }
}
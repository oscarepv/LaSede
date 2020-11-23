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
using System.Net;

namespace LaSede
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaCanchasHoras : ContentPage
    {
        private Models.Canchas cancha;
        private string accion;
        private const string Url = "http://192.168.1.3/laSedeWebService/canchaHoraPost.php";
        private readonly HttpClient canchaHora = new HttpClient();
        private ObservableCollection<Models.CanchasHoras> _post;

        public vistaCanchasHoras(int idCancha, string a)
        {
            InitializeComponent();
            cancha = new Models.Canchas();
            cancha.id = idCancha;
            accion = a;
        }

        protected override void OnAppearing()
        {
            buscarCanchasHoras();
            if (accion.Equals("R"))
            {
                btnNuevo.IsVisible = false;
            }
        }

        private async void buscarCanchasHoras()
        {
            var content = await canchaHora.GetStringAsync(Url + "?id_cancha_j=" + cancha.id);
            // await DisplayAlert("Alerta", cancha.id.ToString(), "Ok");
            List<Models.CanchasHoras> posts = JsonConvert.DeserializeObject<List<Models.CanchasHoras>>(content);
            _post = new ObservableCollection<Models.CanchasHoras>(posts);

            listaCanchasHoras.ItemsSource = _post;
        }

        private async void listaCanchasHoras_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Models.CanchasHoras)e.SelectedItem;
            if (accion.Equals("R"))
            {
                await Navigation.PushAsync(new vistaDetalleReservas(item.id));

            }
            else
            {
                var action = await DisplayAlert("Eliminar", "¿Está seguro de borrar horario?", "Si", "No");
                if (action)
                {
                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            var parametros = new System.Collections.Specialized.NameValueCollection();
                            parametros.Add("id_cancha_hora", (item.id.ToString()));
                            client.UploadValues(Url, "DELETE", client.QueryString = parametros);
                            await DisplayAlert("Alerta", "Eliminado correctamente", "Ok");
                            buscarCanchasHoras();
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Alerta", "Error: " + ex.Message, "ok");
                    }
                    //await Navigation.PushAsync(new vistaDetalleCanchaHora(item.id));
                }
            }
                
        }

        private async void btnNuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new vistaDetalleCanchaHora(cancha.id,"C"));

        }
    }
}
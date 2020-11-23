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

namespace LaSede
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaDetalleCanchaHora : ContentPage
    {
       
        private const string Url = "http://192.168.1.3/laSedeWebService/horarioPost.php";
        private const string UrlCancha = "http://192.168.1.3/laSedeWebService/canchaHoraPost.php";
        private readonly HttpClient horario = new HttpClient();
        HttpClient cliente = new HttpClient();
        private ObservableCollection<Models.Horario> _post;
        private Models.Canchas cancha;
        private string accion;

        public vistaDetalleCanchaHora(int idcancha,string a)
        {
            InitializeComponent();
            cancha = new Models.Canchas();
            cancha.id = idcancha;
            cancha.estado = 0;
            accion = a;
        }

        protected async override void OnAppearing()
        {
            var content = await horario.GetStringAsync(Url);
            List<Models.Horario> posts = JsonConvert.DeserializeObject<List<Models.Horario>>(content);
            _post = new ObservableCollection<Models.Horario>(posts);

            listahorario.ItemsSource = _post;

        }

        private async void listahorario_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (accion.Equals("R")) {
                await Navigation.PushAsync(new vistaCanchasHoras(cancha.id,"H"));
            }
            else
            {
                var item = (Models.Horario)e.SelectedItem;
                //await Navigation.PushAsync(new vistaDetalleHorario(item.id));
                var action = await DisplayAlert("Registrar", "Añadir horario", "Si", "No");
                if (action)
                {
                    try
                    {


                        if (cancha != null)
                        {

                            var parametros = new Dictionary<string, string>();
                            parametros.Add("id_cancha", cancha.id.ToString());
                            parametros.Add("id_horario", item.id.ToString());
                            parametros.Add("estado", cancha.estado.ToString());

                            var req = new HttpRequestMessage(HttpMethod.Post, UrlCancha) { Content = new FormUrlEncodedContent(parametros) };
                            var res = await cliente.SendAsync(req);

                            await DisplayAlert("Alerta", "Hora registrado correctamente", "Ok");
                            await Navigation.PushAsync(new vistaCanchasHoras(cancha.id,"H"));
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
    }
}
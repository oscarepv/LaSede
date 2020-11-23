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

namespace LaSede
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaCanchas : ContentPage
    {
        private string tipoUsuario = "1";
        private string accion ;
        private const string Url = "http://192.168.1.3/laSedeWebService/canchaPost.php";
        private readonly HttpClient cancha = new HttpClient();
        private ObservableCollection<Models.Canchas> _post;
        public vistaCanchas(string a)
        {
            InitializeComponent();
            tipoUsuario = UserSettings.tiposuario;
            accion = a;
        }

        protected async override void OnAppearing()
        {
            var content = await cancha.GetStringAsync(Url);
            List<Models.Canchas> posts = JsonConvert.DeserializeObject<List<Models.Canchas>>(content);
            _post = new ObservableCollection<Models.Canchas>(posts);

            listacanchas.ItemsSource = _post;

            if (!accion.Equals("C"))
            {
                btnNuevo.IsVisible = false;
            }

        }

        private async void listacanchas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Models.Canchas)e.SelectedItem;
            if (accion.Equals("C"))
            {
                await Navigation.PushAsync(new vistaDetalleCanchas(item.id));

            } else if (accion.Equals("R"))
            {
                await Navigation.PushAsync(new vistaCanchasHoras(item.id, "R"));

            }
            else
            {
                await Navigation.PushAsync(new vistaCanchasHoras(item.id,"H"));

            }
        }

        public string prueba()
        {
            return "Prueba hola";
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

        private async void btnNuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new vistaDetalleCanchas(-1));

        }
    }
}
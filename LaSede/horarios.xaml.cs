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


    public partial class horarios : ContentPage
    {
        private string tipoUsuario = "1";
        private const string Url = "http://192.168.1.3/laSedeWebService/horarioPost.php";
        private readonly HttpClient horario = new HttpClient();
        private ObservableCollection<Models.Horario> _post;

        public horarios()
        {
            InitializeComponent();
            tipoUsuario = UserSettings.tiposuario;
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
            var item = (Models.Horario)e.SelectedItem;

            // now you can reference item.Name, item.Location, etc

            // DisplayAlert("ItemSelected" , item.id.ToString(), "Ok");
            await Navigation.PushAsync(new vistaDetalleHorario(item.id));

        }

        private async void btnNuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new vistaDetalleHorario(-1));

        }

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            if (tipoUsuario.Equals(1))
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
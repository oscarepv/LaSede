using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaSede
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaTipos : ContentPage
    {
        private const string Url = "http://192.168.100.161/laSedeWebService/tipoCanchaPost.php";
        private readonly HttpClient cliente = new HttpClient();
        private ObservableCollection<Models.TipoCancha> _post;
        public vistaTipos()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            var content = await cliente.GetStringAsync(Url);
            List<Models.TipoCancha> posts = JsonConvert.DeserializeObject<List<Models.TipoCancha>>(content);
            _post = new ObservableCollection<Models.TipoCancha>(posts);

            listaTipo.ItemsSource = _post;
        }
    }
}
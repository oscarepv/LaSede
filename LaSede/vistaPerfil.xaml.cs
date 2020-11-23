using Newtonsoft.Json;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LaSede.Session;

namespace LaSede
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaPerfil : ContentPage
    {
        HttpClient cliente = new HttpClient();
        private const string Url = "http://192.168.1.3/laSedeWebService/usuarioPost.php";
        private int idCliente;

        public vistaPerfil()
        {
            InitializeComponent();
            idCliente = Convert.ToInt32(UserSettings.userId);
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    var parametros = new System.Collections.Specialized.NameValueCollection();
                    parametros.Add("id_usuario", lblUsuario.Text);
                    parametros.Add("nombres", txtNombre.Text);
                    parametros.Add("apellidos", txtApellido.Text);
                    parametros.Add("documento", txtDocumento.Text);
                    parametros.Add("telefono", txtTelefono.Text);
                    parametros.Add("correo", txtCorreo.Text);
                    parametros.Add("url_foto", txtUrl.Text);
                    parametros.Add("password", txtRePasswd.Text);

                    client.UploadValues(Url, "PUT", client.QueryString = parametros);
                }
                await DisplayAlert("Alerta", "Dato Actualizado correctamente", "ok");

            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "Error: " + ex.Message, "ok");
            }
        }

        private async void btnFoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "foto.jpg",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front
            });

            if (file == null)
                return;

            //await DisplayAlert("File Location", file.Path, "OK");
            txtUrl.Text = file.Path;

            foto.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        protected async override void OnAppearing()
        {
            var content = await cliente.GetStringAsync(Url + "?id_usuario=" + idCliente);
            Models.Usuario usuario = JsonConvert.DeserializeObject<Models.Usuario>(content);
            txtApellido.Text = usuario.apellidos;
            txtNombre.Text = usuario.nombres;
            txtDocumento.Text = usuario.documento;
            txtTelefono.Text = usuario.telefono;
            txtCorreo.Text = usuario.correo;
            txtRePasswd.Text = usuario.password;
            lblUsuario.Text = usuario.id_Usuario;
            foto.Source = usuario.url_foto;
            txtUrl.Text = usuario.url_foto;
        }
    }
}
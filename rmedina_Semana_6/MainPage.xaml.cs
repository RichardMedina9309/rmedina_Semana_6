using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace rmedina_Semana_6
{
    public partial class MainPage : ContentPage
    {
        private const string Url = "http://172.24.80.1/semana_5/post.php";
        private readonly HttpClient cliente = new HttpClient();
        private ObservableCollection<rmedina_Semana_6.WS.Datos> post;

        public MainPage()
        {
            InitializeComponent();
            Obtener();
        }

        public async void Obtener()
        {
            var content = await cliente.GetStringAsync(Url);
            List<rmedina_Semana_6.WS.Datos> posts = JsonConvert.DeserializeObject<List<rmedina_Semana_6.WS.Datos>>(content);
            post = new ObservableCollection<WS.Datos>(posts);
            lista.ItemsSource = post;
        }

        private void btnRegistro_Clicked(object sender, EventArgs e)
        {
            var mensaje = "bienvenido";
            DependencyService.Get<Mensaje>().longAlert(mensaje);
            Navigation.PushAsync(new Registro());
        }

        private void lista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var objt = (WS.Datos)e.SelectedItem;
            var item = objt.codigo.ToString();
            int ID = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new Editar(objt));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

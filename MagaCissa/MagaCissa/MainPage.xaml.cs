using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Acr.UserDialogs;
using MagaCissa.Droid;

namespace MagaCissa
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Consultar(object sender, EventArgs e)
        {
            actIndicator2.HorizontalOptions = LayoutOptions.CenterAndExpand;
            actIndicator2.Color = Color.White;
            actIndicator2.IsRunning = true;
            actIndicator2.IsVisible = true;
            frameLoading.IsVisible = true;

            await RealizarConsulta();

            actIndicator2.IsRunning = false;
            actIndicator2.IsVisible = false;
            frameLoading.IsVisible = false;
        }

        private async Task RealizarConsulta()
        {
            string url = "http://cissamagazine.com.br/cliente/pedidos?verPedido=0011811030995&cpfcnpj=123.456.789-10";
            if (url != null)
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = await request.GetResponseAsync();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string Texto = reader.ReadToEnd();

                int indexDaTagDeInicioDaSituacao = Texto.IndexOf("<div class=\"situacao\">");
                Texto = Texto.Substring(indexDaTagDeInicioDaSituacao);
                int indexDoFinalDaTagDeSituacao = Texto.IndexOf("</div>");
                Texto = Texto.Substring(0, indexDoFinalDaTagDeSituacao);

                int indexInicio = Texto.IndexOf("\n");
                Texto = Texto.Substring(indexInicio);
                int indexFinal = Texto.IndexOf("\n");
                Texto = Texto.Substring(2);

                if (Texto != null)
                    lblStatus.Text = Texto.Trim();
            }
        }
    }
}

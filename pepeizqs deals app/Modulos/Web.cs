using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System;
using static pepeizqs_deals_app.MainWindow;

namespace Modulos
{
    public static class Web
    {
        public static string dominio = "https://pepeizqdeals.com";

        public static void Cargar()
        {
			ObjetosVentana.wvWeb.Source = new Uri(dominio + "/Identity/Account/Login");

            ObjetosVentana.wvWeb.NavigationCompleted += CompletarCarga;
        }

        private static async void CompletarCarga(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
			WebView2 wv = (WebView2)sender;

			if (wv.Source == new Uri(dominio + "/Identity/Account/Login"))
			{
				string dato1 = "pepeizq@msn.com";
				string dato2 = "Pepelu605*";

				string email = "document.getElementById('Input_Email').value = '" + dato1 + "'";

				await wv.ExecuteScriptAsync(email);

				string contraseña = "document.getElementById('Input_Password').value = '" + dato2 + "'";

				await wv.ExecuteScriptAsync(contraseña);

				string click = "document.getElementById('login-submit').click();";

				//await wv.ExecuteScriptAsync(click);
			}
		}
    }
}

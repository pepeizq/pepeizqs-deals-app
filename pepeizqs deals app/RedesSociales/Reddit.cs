using Microsoft.UI.Xaml;
using Modulos;
using System;
using System.Threading.Tasks;
using static pepeizqs_deals_app.MainWindow;

namespace RedesSociales
{
	public static class Reddit
	{
		public static string cuenta;

		public static async void Cargar()
		{
			Random rand = new Random();
			int azar = 0;
			
			char letra;
			for (int i = 0; i < 20; i++)
			{
				azar = rand.Next(0, 26);
				letra = Convert.ToChar(azar + 65);
				cuenta = cuenta + letra;
			}

			if (string.IsNullOrEmpty(cuenta) == false)
			{
				cuenta = cuenta.ToLower();
			}

			ObjetosVentana.botonRedditArrancar.Click += ArrancarClick;
			ObjetosVentana.botonRedditSumar.Click += SumarClick;

			await ObjetosVentana.wvReddit.EnsureCoreWebView2Async(null);
			ObjetosVentana.wvReddit.CoreWebView2.DOMContentLoaded += CompletarCarga;
		}

		private static void ArrancarClick(object sender, RoutedEventArgs e)
		{
			ObjetosVentana.wvReddit.CoreWebView2.Navigate("https://www.reddit.com/register/");
			ObjetosVentana.tbRedditEnlace.Text = ObjetosVentana.wvReddit.CoreWebView2.Source;
		}

		private static async void CompletarCarga(object sender, object e)
		{
			Microsoft.Web.WebView2.Core.CoreWebView2 wv = (Microsoft.Web.WebView2.Core.CoreWebView2)sender;

			ObjetosVentana.tbRedditEnlace.Text = wv.Source;

			if (wv.Source == "https://www.reddit.com/register/")
			{
				await wv.ExecuteScriptAsync("document.getElementsByName('emailPermission')[0].checked = true;");

				await Task.Delay(1000);

				await wv.ExecuteScriptAsync("document.getElementsByName('email')[0].focus();");
				await wv.ExecuteScriptAsync("document.getElementsByName('email')[0].value = '" + DatosPersonales.RedditCorreo + "'");

				await wv.ExecuteScriptAsync("document.getElementsByName('username')[0].value = '" + cuenta + "'");

				await wv.ExecuteScriptAsync("document.getElementsByName('password')[0].value = '" + DatosPersonales.RedditContraseña + "'");
			}
		}

		private static void SumarClick(object sender, RoutedEventArgs e)
		{
			RedditSharp.Reddit cliente = new RedditSharp.Reddit();
			cliente.LogIn(cuenta, DatosPersonales.RedditContraseña);

			cliente.InitOrUpdateUser();

			if (cliente.User != null)
			{
				RedditSharp.Things.Subreddit sub1 = cliente.GetSubreddit("/r/pepeizqdeals");
				sub1.Subscribe();

				RedditSharp.Things.Subreddit sub2 = cliente.GetSubreddit("/r/gamesdealssteam");
				sub2.Subscribe();

				RedditSharp.Things.Subreddit sub3 = cliente.GetSubreddit("/r/gamesdealsgog");
				sub3.Subscribe();
			}
		}
	}
}

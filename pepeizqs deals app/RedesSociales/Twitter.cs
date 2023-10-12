using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using Modulos;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Web;
using Tweetinvi.Models;
using static pepeizqs_deals_app.MainWindow;

namespace RedesSociales
{
	public static class Twitter
	{
		public async static void Cargar()
		{
			TwitterClient cliente = new TwitterClient("4c67yvUZOS4mAfZAM0ixsNDUA", "XmMEJukGa3vKwPJIMZknd7GLgKok54sucYuYelRXIT5yRKunuW",
				"1030738433105387520-U2pYtW6lZJdkHsI2Y9rJoBuuwtkmsi", "Du0nKe6HygQrfgXHwJTybLzfTt7G6IToLeVCxSczrPFLV");
		
			ObjetosVentana.tbTwitterCodigo.Tag = cliente;

			ObjetosVentana.wvTwitter.NavigationCompleted += CompletarCarga;

			IAuthenticationRequest peticion = await cliente.Auth.RequestAuthenticationUrlAsync();

			ObjetosVentana.wvTwitter.Source = new Uri(peticion.AuthorizationURL);
			ObjetosVentana.wvTwitter.Tag = peticion;
		}

		private static async void CompletarCarga(object sender, CoreWebView2NavigationCompletedEventArgs e)
		{
			WebView2 wv = (WebView2)sender;

			if (wv.Source.ToString().Contains("https://api.twitter.com/oauth/authorize") == true)
			{
				try
				{
					await wv.ExecuteScriptAsync("document.getElementById('allow').click();");
				}
				catch { }
				
				try
				{
					string html = await wv.ExecuteScriptAsync("document.documentElement.outerHTML");

					if (html != null)
					{
						html = Regex.Unescape(html);
						html = html.Remove(0, 1);
						html = html.Remove(html.Length - 1, 1);

						if (html.Contains("<code>") == true)
						{
							int int1 = html.IndexOf("<code>");
							string temp1 = html.Remove(0, int1 + 6);

							int int2 = temp1.IndexOf("</code>");
							string temp2 = temp1.Remove(int2, temp1.Length - int2);

							ObjetosVentana.tbTwitterCodigo.Text = temp2.Trim();
						}
					}
				}
				catch { }
			}
		}

		public static async void Enviar(Noticia noticia)
		{
			TwitterClient cliente = (TwitterClient)ObjetosVentana.tbTwitterCodigo.Tag;

			try
			{
				ITwitterResult resultado = await PonerTweet(cliente,
					new TweetV2PostRequest
					{
						Text = noticia.TituloEn + " " + Environment.NewLine + Environment.NewLine + RSS.BuscarEnlace(noticia)
					}
				);
			}
			catch { }		
		}

		public static Task<ITwitterResult> PonerTweet(TwitterClient cliente, TweetV2PostRequest parametros)
		{
			return cliente.Execute.AdvanceRequestAsync(
				(ITwitterRequest peticion) =>
				{
					string json = cliente.Json.Serialize(parametros);
					StringContent contenido = new StringContent(json, Encoding.UTF8, "application/json");

					peticion.Query.Url = "https://api.twitter.com/2/tweets";
					peticion.Query.HttpMethod = Tweetinvi.Models.HttpMethod.POST;
					peticion.Query.HttpContent = contenido;
				}
			);
		}

		public class TweetV2PostRequest
		{
			[JsonProperty("text")]
			public string Text { get; set; } = string.Empty;
		}
	}
}

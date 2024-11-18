using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualBasic;
using Microsoft.Web.WebView2.Core;
using Modulos;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using static pepeizqs_deals_app.MainWindow;

namespace RedesSociales
{
    public static class Steam
    {
        public static void Cargar()
        {
            ObjetosVentana.wvSteam.Source = new Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create");

			ObjetosVentana.wvSteam.NavigationCompleted += CompletarCarga;
		}

		private static async void CompletarCarga(object sender, object e)
        {
            WebView2 wv = (WebView2)sender;

            ObjetosVentana.tbSteamEnlace.Text = wv.Source.AbsoluteUri;

            if (wv.CoreWebView2.DocumentTitle == "Steam Community :: Error")
            {
				await Task.Delay(15000);
				wv.Source = new Uri("https://steamcommunity.com/login/home/?goto=groups%2Fpepeizqdeals%2Fannouncements%2Fcreate");
            }
            else
            {
				if (wv.Source.AbsoluteUri.Contains("https://steamcommunity.com/login/home/?goto=groups%2Fpepeizqdeals%2Fannouncements%2Fcreate") == true)
				{
					await Task.Delay(5000);
					await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_TextInput_2eKVn')[0].value = '" + DatosPersonales.SteamLogin + "'");

					await Task.Delay(2000);
					await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_TextInput_2eKVn')[1].value = '" + DatosPersonales.SteamContraseña + "'");

					await Task.Delay(2000);
					//await wv.ExecuteScriptAsync("document.getElementsByClassName('newlogindialog_LoginForm_3Tsg9')[0].submit();");
				}
				else
				{
					if (wv.Source != new Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create"))
					{
						await Task.Delay(5000);
						wv.Source = new Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create");
					}					
				}
			}           
        }

		public static async void Enviar(Noticia noticia)
		{
			WebView2 wv = ObjetosVentana.wvSteam;

			if (wv.Source.AbsoluteUri == "https://steamcommunity.com/groups/pepeizqdeals/announcements/create")
			{
				await Task.Delay(1000);
				await wv.ExecuteScriptAsync("document.getElementById('headline').focus();");
				await wv.ExecuteScriptAsync("document.getElementById('headline').value = '" + HttpUtility.JavaScriptStringEncode(WebUtility.HtmlDecode(noticia.TituloEn)) + "'");

				await Task.Delay(1000);
				await wv.ExecuteScriptAsync("document.getElementById('body').focus();");

				await Task.Delay(1000);
				await wv.ExecuteScriptAsync("document.getElementById('body').value = '" + HttpUtility.JavaScriptStringEncode(GenerarContenido("https://pepeizqdeals.com/news/" + noticia.Id.ToString() + "/", noticia.ContenidoEn, WebUtility.HtmlDecode(noticia.Imagen))) + "'");

				await Task.Delay(1000);

				await wv.ExecuteScriptAsync("document.getElementById('language').focus();");
				await wv.ExecuteScriptAsync("document.getElementById('language').selectedIndex = '5';");
				await wv.ExecuteScriptAsync("document.getElementById('language').onchange();");

				await Task.Delay(1000);

				await wv.ExecuteScriptAsync("document.getElementById('headline').focus();");
				await wv.ExecuteScriptAsync("document.getElementById('headline').value = '" + HttpUtility.JavaScriptStringEncode(WebUtility.HtmlDecode(noticia.TituloEs)) + "'");

				await Task.Delay(1000);

				await wv.ExecuteScriptAsync("document.getElementById('body').value = '" + HttpUtility.JavaScriptStringEncode(GenerarContenido("https://pepeizqdeals.com/news/" + noticia.Id.ToString() + "/", noticia.ContenidoEs, WebUtility.HtmlDecode(noticia.Imagen))) + "'");

				await Task.Delay(1000);

				await wv.ExecuteScriptAsync("document.getElementsByClassName('btn_green_white_innerfade btn_medium')[0].focus();");
				await wv.ExecuteScriptAsync("document.getElementsByClassName('btn_green_white_innerfade btn_medium')[0].click();");

				await Task.Delay(10000);

				wv.Source = new Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create");
			}
			else
			{
				wv.Source = new Uri("https://steamcommunity.com/groups/pepeizqdeals/announcements/create");
			}
		}

		private static string GenerarContenido(string enlace, string contenido2, string imagen)
		{
			string contenido = "[url=" + enlace + "]Link[/url]" + Environment.NewLine + Environment.NewLine;

			contenido = contenido + WebUtility.HtmlDecode(contenido2);

			int i = 0;
			while (i < 1000)
			{
				if (contenido.Contains("<div") == true)
				{
					int int1 = contenido.IndexOf("<div");
					string temp1 = contenido.Remove(0, int1);

					int int2 = temp1.IndexOf(">");
					contenido = contenido.Remove(int1, int2 + 1);
				}
				else
				{
					break;
				}

				i += 1;
			}

			i = 0;
			while (i < 1000)
			{
				if (contenido.Contains("<a") == true)
				{
					int int1 = contenido.IndexOf("<a");
					string temp1 = contenido.Remove(0, int1);

					int int2 = temp1.IndexOf(Strings.ChrW(34));
					contenido = contenido.Remove(int1, int2 + 1);

					contenido = contenido.Insert(int1, "[url=");

					int int3 = contenido.IndexOf(Strings.ChrW(34));
					string temp3 = contenido.Remove(0, int3);

					int int4 = temp3.IndexOf(">");
					contenido = contenido.Remove(int3, int4 + 1);

					contenido = contenido.Insert(int3, "]");
				}
				else
				{
					break;
				}

				i += 1;
			}

			i = 0;
			while (i < 1000)
			{
				if (contenido.Contains("<img") == true)
				{
					int int1 = contenido.IndexOf("<img");
					string temp1 = contenido.Remove(0, int1);

					int int2 = temp1.IndexOf(Strings.ChrW(34));
					contenido = contenido.Remove(int1, int2 + 1);

					contenido = contenido.Insert(int1, "[img=");

					int int3 = contenido.IndexOf(Strings.ChrW(34));
					string temp3 = contenido.Remove(0, int3);

					int int4 = temp3.IndexOf(">");
					contenido = contenido.Remove(int3, int4 + 1);

					contenido = contenido.Insert(int3, "]");
				}
				else
				{
					break;
				}

				i += 1;
			}

			contenido = contenido.Replace("</div>", Environment.NewLine + Environment.NewLine);
			contenido = contenido.Replace("<ul>", "[list]");
			contenido = contenido.Replace("</ul>", "[/list]");
			contenido = contenido.Replace("<li>", "[*]");
			contenido = contenido.Replace("</li>", null);
			contenido = contenido.Replace("</a>", "[/url]");
			contenido = contenido.Replace("<br/>", null);

			if (imagen != null)
			{
				contenido = "[img=" + imagen + "]" + Environment.NewLine + Environment.NewLine + contenido;
			}

			return contenido;
		}
	}
}

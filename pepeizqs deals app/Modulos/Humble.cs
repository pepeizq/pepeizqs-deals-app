using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System;
using System.Threading.Tasks;
using static pepeizqs_deals_app.MainWindow;

namespace Modulos
{
	public static class Humble
	{
        private static string html = string.Empty;
		private static int pagina = 0;
		private static int numPaginas = 0;

        public static void Cargar()
		{
			ObjetosVentana.botonHumbleArrancar.Click += ArrancarClick;
			ObjetosVentana.botonHumbleArrancar.PointerEntered += Animaciones.EntraRatonBoton2;
			ObjetosVentana.botonHumbleArrancar.PointerExited += Animaciones.SaleRatonBoton2;

			ObjetosVentana.wvHumble.NavigationCompleted += CompletarCarga;
		}

		private static void ArrancarClick(object sender, RoutedEventArgs e)
		{
			ObjetosVentana.wvHumble.Source = new Uri("https://www.humblebundle.com/store/api/search?filter=onsale&sort=discount&request=2&page_size=20&page=0");
		}

		private static async void CompletarCarga(object sender, CoreWebView2NavigationCompletedEventArgs e)
		{
			ObjetosVentana.tbHumblePaginas.Text = pagina.ToString() + "/" + numPaginas.ToString();

            WebView2 wv = (WebView2)sender;

            if (wv.Source.AbsoluteUri.Contains("https://www.humblebundle.com/store/api/") == true)
			{
				html = await wv.CoreWebView2.ExecuteScriptAsync("document.documentElement.outerHTML");

				if (html != null)
				{
					if (html.Contains("{"))
					{
						int int1 = html.IndexOf("{");
						html = html.Remove(0, int1);
					}

					if (html.Contains("}]}") == true)
					{
						int int1 = html.LastIndexOf("}]}");
						html = html.Remove(int1, html.Length - int1);

						html = html + "}]}";
					}

					if (html != "null")
					{
						if (numPaginas == 0)
						{
							string temp1 = html;

							int int2 = temp1.IndexOf("num_pages");
							string temp2 = temp1.Remove(0, int2);

							int int3 = temp2.IndexOf(":");
							string temp3 = temp2.Remove(0, int3 + 1);

							int int4 = temp3.IndexOf(",");
							string temp4 = temp3.Remove(int4, temp3.Length - int4);

							numPaginas = int.Parse(temp4);
						}
					}
                }

				await Task.Delay(2000);

				wv.Source = new Uri(Web.dominio + "/Admin/Humble");
            }
			else if (wv.Source == new Uri(Web.dominio + "/Admin/Humble"))
			{
                if (pagina < numPaginas)
                {
					if (string.IsNullOrEmpty(html) == false)
					{
                        html = html.Replace("'", "");

                        if (html == "null")
						{
                            pagina -= 1;
                        }
						else
						{
                            string inyectarhtml = "document.getElementById('humble-texto').value = '" + html + "'";
                            html = null;

                            await wv.ExecuteScriptAsync(inyectarhtml);

                            await Task.Delay(2000);

                            string click = "document.getElementById('humble-cargar').click();";

                            await wv.ExecuteScriptAsync(click);

                            await Task.Delay(2000);

                            pagina += 1;
                            wv.Source = new Uri("https://www.humblebundle.com/store/api/search?filter=onsale&sort=discount&request=2&page_size=20&page=" + pagina.ToString());
                        }
					}
				}
				else
				{

				}
			}
        }
	}
}

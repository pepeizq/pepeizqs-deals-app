using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static pepeizqs_deals_app.MainWindow;

namespace Modulos
{
	public static class Epic
	{
		private static string html = string.Empty;
		private static int pagina = 0;
		private static int numJuegos = 0;
		private static string fecha = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();

		public static void Cargar()
		{
			ObjetosVentana.botonEpicArrancar.Click += ArrancarClick;
			ObjetosVentana.botonEpicArrancar.PointerEntered += Animaciones.EntraRatonBoton2;
			ObjetosVentana.botonEpicArrancar.PointerExited += Animaciones.SaleRatonBoton2;

			ObjetosVentana.wvEpicAPI.NavigationCompleted += CompletarCarga;
		}

		private static void ArrancarClick(object sender, RoutedEventArgs e)
		{
			ObjetosVentana.wvEpicAPI.Source = new Uri("https://store.epicgames.com/graphql?operationName=searchStoreQuery&variables={%22allowCountries%22:%22ES%22,%22category%22:%22games/edition/base|addons|games/edition%22,%22count%22:40,%22country%22:%22ES%22,%22effectiveDate%22:%22[," + fecha + "T10:00:00.141Z]%22,%22keywords%22:%22%22,%22locale%22:%22en-GB%22,%22onSale%22:true,%22sortBy%22:%22relevancy,viewableDate%22,%22sortDir%22:%22DESC,DESC%22,%22start%22:" + pagina + ",%22tag%22:%22%22,%22withPrice%22:true}&extensions={%22persistedQuery%22:{%22version%22:1,%22sha256Hash%22:%227d58e12d9dd8cb14c84a3ff18d360bf9f0caa96bf218f2c5fda68ba88d68a437%22}}");
		}

		private static async void CompletarCarga(object sender, object e)
		{
			bool parar = false;

			ObjetosVentana.tbEpicPaginas.Text = pagina.ToString();

			WebView2 wv = (WebView2)sender;

			if (wv.Source.AbsoluteUri.Contains("https://store.epicgames.com/graphql?operationName=searchStoreQuery") == true)
			{
				html = await wv.CoreWebView2.ExecuteScriptAsync("document.documentElement.outerHTML");

				if (string.IsNullOrEmpty(html) == false)
				{
					html = System.Text.RegularExpressions.Regex.Unescape(html);

					if (html.Contains("<pre>") == true)
					{
						int int1 = html.IndexOf("<pre>");
						html = html.Remove(0, int1 + 5);
					}

					if (html.Contains("</pre>") == true)
					{
						int int1 = html.LastIndexOf("</pre>");
						html = html.Remove(int1, html.Length - int1);
					}

					using (SqlConnection conexion = new SqlConnection(DatosPersonales.Servidor))
					{
						conexion.Open();

						if (conexion.State == System.Data.ConnectionState.Open)
						{
							string sqlAñadir = "INSERT INTO temporalepictienda " +
										"(contenido, fecha, enlace) VALUES " +
										"(@contenido, @fecha, @enlace) ";

							using (SqlCommand comando = new SqlCommand(sqlAñadir, conexion))
							{
								comando.Parameters.AddWithValue("@contenido", html);
								comando.Parameters.AddWithValue("@fecha", DateTime.Now);
								comando.Parameters.AddWithValue("@enlace", pagina);

								try
								{
									comando.ExecuteNonQuery();
								}
								catch
								{

								}
							}
						}
					}
				}
			}

			if (html.Contains("elements\":[]") == true)
			{
				parar = true;
			}

			if (parar == false)
			{
				await Task.Delay(1000);

				html = null;
				pagina += 1;
				wv.Source = new Uri("https://store.epicgames.com/graphql?operationName=searchStoreQuery&variables={%22allowCountries%22:%22ES%22,%22category%22:%22games/edition/base|addons|games/edition%22,%22count%22:40,%22country%22:%22ES%22,%22effectiveDate%22:%22[," + fecha + "T10:00:00.141Z]%22,%22keywords%22:%22%22,%22locale%22:%22en-GB%22,%22onSale%22:true,%22sortBy%22:%22relevancy,viewableDate%22,%22sortDir%22:%22DESC,DESC%22,%22start%22:" + pagina + ",%22tag%22:%22%22,%22withPrice%22:true}&extensions={%22persistedQuery%22:{%22version%22:1,%22sha256Hash%22:%227d58e12d9dd8cb14c84a3ff18d360bf9f0caa96bf218f2c5fda68ba88d68a437%22}}");
			}
		}
	}

	public class EpicGamesStorePrincipal
	{
		[JsonPropertyName("data")]
		public EpicGamesStoreData Datos { get; set; }
	}

	public class EpicGamesStoreData
	{
		[JsonPropertyName("Catalog")]
		public EpicGamesStoreCatalog Catalogo { get; set; }
	}

	public class EpicGamesStoreCatalog
	{
		[JsonPropertyName("searchStore")]
		public EpicGamesStoreSearch Busqueda { get; set; }
	}

	public class EpicGamesStoreSearch
	{
		[JsonPropertyName("elements")]
		public List<EpicGamesStoreJuego> Juegos { get; set; }
	}

	public class EpicGamesStoreJuego
	{
		[JsonPropertyName("title")]
		public string Nombre { get; set; }

		[JsonPropertyName("keyImages")]
		public List<EpicGamesStoreJuegoImagen> Imagenes { get; set; }

		[JsonPropertyName("offerMappings")]
		public List<EpicGamesStoreJuegoEnlace> Enlaces { get; set; }

		[JsonPropertyName("price")]
		public EpicGamesStoreJuegoPrecio Precio { get; set; }

		[JsonPropertyName("productSlug")]
		public string Enlace { get; set; }

		[JsonPropertyName("catalogNs")]
		public EpicGamesStoreJuegoEnlaceMapeo Enlaces2 { get; set; }
	}

	public class EpicGamesStoreJuegoImagen
	{
		[JsonPropertyName("type")]
		public string Tipo { get; set; }

		[JsonPropertyName("url")]
		public string Enlace { get; set; }
	}

	public class EpicGamesStoreJuegoEnlace
	{
		[JsonPropertyName("pageSlug")]
		public string Slug { get; set; }
	}

	public class EpicGamesStoreJuegoPrecio
	{
		[JsonPropertyName("totalPrice")]
		public EpicGamesStoreJuegoPrecio2 PrecioTotal { get; set; }
	}

	public class EpicGamesStoreJuegoPrecio2
	{
		[JsonPropertyName("fmtPrice")]
		public EpicGamesStoreJuegoPrecio3 PrecioFmt { get; set; }
	}

	public class EpicGamesStoreJuegoPrecio3
	{
		[JsonPropertyName("originalPrice")]
		public string PrecioBase { get; set; }

		[JsonPropertyName("discountPrice")]
		public string PrecioRebajado { get; set; }
	}

	public class EpicGamesStoreJuegoEnlaceMapeo
	{
		[JsonPropertyName("mappings")]
		public List<EpicGamesStoreJuegoEnlace> Mapeos { get; set; }
	}
}

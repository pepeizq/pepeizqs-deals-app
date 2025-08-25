using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json;
using System.Threading.Tasks;
using static pepeizqs_deals_app.MainWindow;

namespace Modulos
{
	public static class AmazonLuna
	{
		private static string html = string.Empty;

		public static void Cargar()
		{
			ObjetosVentana.botonAmazonLunaArrancar.Click += ArrancarClick;
			ObjetosVentana.botonAmazonLunaArrancar.PointerEntered += Animaciones.EntraRatonBoton2;
			ObjetosVentana.botonAmazonLunaArrancar.PointerExited += Animaciones.SaleRatonBoton2;

			ObjetosVentana.wvAmazonLuna.NavigationCompleted += CompletarCarga;
		}

		private static void ArrancarClick(object sender, RoutedEventArgs e)
		{
			ObjetosVentana.wvAmazonLuna.Source = new Uri("https://luna.amazon.es/subscription/luna-plus/B085TRCCT6");
		}

		private static async void CompletarCarga(object sender, object e)
		{
			WebView2 wv = (WebView2)sender;

			if (wv.Source.AbsoluteUri.Contains("https://luna.amazon.es/subscription/luna-plus/B085TRCCT6") == true)
			{
				await Task.Delay(3000);

				html = await wv.ExecuteScriptAsync("document.documentElement.outerHTML");

				if (string.IsNullOrEmpty(html) == false)
				{
					html = System.Text.RegularExpressions.Regex.Unescape(html);

					if (html.Contains(Strings.ChrW(34) + "collection_channel_games_lunaplus" + Strings.ChrW(34)) == true)
					{
						html = html.Substring(html.IndexOf(Strings.ChrW(34) + "collection_channel_games_lunaplus" + Strings.ChrW(34)));
					}

					List<AmazonLunaJuego> juegos = new List<AmazonLunaJuego>();

					int i = 0;
					while (i < 1000)
					{
						if (html.Contains("id=" + Strings.ChrW(34) + "game_tile_amzn1.adg.product.") == true)
						{
							int int1 = html.IndexOf("id=" + Strings.ChrW(34) + "game_tile_amzn1.adg.product.");
							string temp1 = html.Remove(0, int1 + 4);

							html = html.Remove(0, int1 + 4);

							int int2 = temp1.IndexOf(Strings.ChrW(34));
							string temp2 = temp1.Remove(int2, temp1.Length - int2);

							if (temp2.Contains("_impression") == false)
							{
								int int3 = temp1.IndexOf("title=" + Strings.ChrW(34));
								string temp3 = temp1.Remove(0, int3 + 7);

								int int4 = temp3.IndexOf(Strings.ChrW(34));
								string temp4 = temp3.Remove(int4, temp3.Length - int4);

								AmazonLunaJuego juego = new AmazonLunaJuego
								{
									Id = temp2,
									Nombre = temp4
								};

								juegos.Add(juego);
							}
							
						}

						i += 1;
					}

					using (SqlConnection conexion = new SqlConnection(DatosPersonales.Servidor))
					{
						conexion.Open();

						if (conexion.State == System.Data.ConnectionState.Open)
						{
							string sqlAñadir = "INSERT INTO temporalamazonluna " +
										"(contenido, fecha, enlace) VALUES " +
										"(@contenido, @fecha, @enlace) ";

							using (SqlCommand comando = new SqlCommand(sqlAñadir, conexion))
							{
								comando.Parameters.AddWithValue("@contenido", JsonSerializer.Serialize(juegos));
								comando.Parameters.AddWithValue("@fecha", DateTime.Now);
								comando.Parameters.AddWithValue("@enlace", "1");

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

					ObjetosVentana.tbAmazonLuna.Text = "Cargados " + juegos.Count.ToString() + " juegos";
				}
			}
		}
	}

	public class AmazonLunaJuego
	{
		public string Id { get; set; }
		public string Nombre { get; set; }
	}
}

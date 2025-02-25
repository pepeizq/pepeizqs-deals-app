﻿using FontAwesome6.Fonts;
using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using static pepeizqs_deals_app.MainWindow;

namespace Modulos
{
	public static class RSS
	{
		public static string dominio = "https://pepeizqdeals.com";

		public static void Cargar()
		{
            ObjetosVentana.wvRSS.NavigationCompleted += CompletarCarga;

            ObjetosVentana.wvRSS.Source = new Uri("https://pepeizqdeals.com/news-rss"); 
        }

        private static async void CompletarCarga(object sender, object e)
        {
            WebView2 wv = (WebView2)sender;

            if (wv.Source.AbsoluteUri.Contains("https://pepeizqdeals.com/news-rss") == true)
            {
                string html = await wv.CoreWebView2.ExecuteScriptAsync("document.documentElement.outerHTML");

                if (html != null)
                {
					if (WebUtility.HtmlDecode(html).Contains("<body>The service is unavailable.</body>") == false)
					{
						html = JsonConvert.DeserializeObject(html).ToString();

						int int1 = html.IndexOf("[{");
						html = html.Remove(0, int1);

						int int2 = html.IndexOf("}]");
						html = html.Remove(int2 + 2, html.Length - int2 - 2);

						List<Noticia> noticias = JsonConvert.DeserializeObject<List<Noticia>>(html);

						if (noticias != null)
						{
							if (noticias.Count > 0)
							{
								wv.Visibility = Visibility.Collapsed;
								ObjetosVentana.spRSSNoticias.Visibility = Visibility.Visible;
								ObjetosVentana.spRSSNoticias.Children.Clear();

								foreach (var noticia in noticias)
								{
									Grid grid = new Grid
									{
										Padding = new Thickness(20),
										BorderThickness = new Thickness(0, 0, 0, 1)
									};

									ColumnDefinition col1 = new ColumnDefinition
									{
										Width = new GridLength(1, GridUnitType.Star)
									};

									grid.ColumnDefinitions.Add(col1);

									ColumnDefinition col2 = new ColumnDefinition
									{
										Width = new GridLength(1, GridUnitType.Auto)
									};

									grid.ColumnDefinitions.Add(col2);

									TextBlock tb = new TextBlock();
									tb.Text = noticia.TituloEn;

									tb.SetValue(Grid.ColumnProperty, 0);
									grid.Children.Add(tb);

									//--------------------------------------------

									StackPanel sp = new StackPanel
									{
										Orientation = Orientation.Horizontal
									};

									#region Todo
									TextBlock tbTodo = new TextBlock
									{
										Text = "Todo",
										Foreground = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["ColorFuente"])
									};

									Button2 botonTodo = new Button2
									{
										Tag = noticia,
										Content = tbTodo,
										Margin = new Thickness(20, 0, 0, 0),
										Background = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["ColorPrimario"])
									};

									botonTodo.Click += ClickTodo;
									botonTodo.PointerEntered += Animaciones.EntraRatonBoton2;
									botonTodo.PointerExited += Animaciones.SaleRatonBoton2;

									sp.Children.Add(botonTodo);
									#endregion

									#region Discord
									FontAwesome iconoDiscord = new FontAwesome
									{
										Icon = FontAwesome6.EFontAwesomeIcon.Brands_Discord,
										Foreground = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["ColorFuente"])
									};

									Button2 botonDiscord = new Button2
									{
										Tag = noticia,
										Content = iconoDiscord,
										Margin = new Thickness(20, 0, 0, 0),
										Background = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["ColorPrimario"])
									};

									botonDiscord.Click += ClickDiscord;
									botonDiscord.PointerEntered += Animaciones.EntraRatonBoton2;
									botonDiscord.PointerExited += Animaciones.SaleRatonBoton2;

									sp.Children.Add(botonDiscord);
									#endregion

									#region Telegram
									FontAwesome iconoTelegram = new FontAwesome
									{
										Icon = FontAwesome6.EFontAwesomeIcon.Brands_Telegram,
										Foreground = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["ColorFuente"])
									};

									Button2 botonTelegram = new Button2
									{
										Tag = noticia,
										Content = iconoTelegram,
										Margin = new Thickness(20, 0, 0, 0),
										Background = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["ColorPrimario"])
									};

									botonTelegram.Click += ClickTelegram;
									botonTelegram.PointerEntered += Animaciones.EntraRatonBoton2;
									botonTelegram.PointerExited += Animaciones.SaleRatonBoton2;

									sp.Children.Add(botonTelegram);
									#endregion

									#region Reddit
									FontAwesome iconoReddit = new FontAwesome
									{
										Icon = FontAwesome6.EFontAwesomeIcon.Brands_Reddit,
										Foreground = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["ColorFuente"])
									};

									Button2 botonReddit = new Button2
									{
										Tag = noticia,
										Content = iconoReddit,
										Margin = new Thickness(20, 0, 0, 0),
										Background = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["ColorPrimario"])
									};

									botonReddit.Click += ClickReddit;
									botonReddit.PointerEntered += Animaciones.EntraRatonBoton2;
									botonReddit.PointerExited += Animaciones.SaleRatonBoton2;

									sp.Children.Add(botonReddit);
									#endregion

									#region Steam
									FontAwesome iconoSteam = new FontAwesome
									{
										Icon = FontAwesome6.EFontAwesomeIcon.Brands_Steam,
										Foreground = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["ColorFuente"])
									};

									Button2 botonSteam = new Button2
									{
										Tag = noticia,
										Content = iconoSteam,
										Margin = new Thickness(20, 0, 0, 0),
										Background = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["ColorPrimario"])
									};

									botonSteam.Click += ClickSteam;
									botonSteam.PointerEntered += Animaciones.EntraRatonBoton2;
									botonSteam.PointerExited += Animaciones.SaleRatonBoton2;

									sp.Children.Add(botonSteam);
									#endregion

									sp.SetValue(Grid.ColumnProperty, 1);
									grid.Children.Add(sp);

									ObjetosVentana.spRSSNoticias.Children.Add(grid);
								}
							}
						}
					}                  
                }
            }
        }

        public static string BuscarEnlace(Noticia noticia)
        {
            string enlace = string.Empty;

            if (noticia.Enlace != null)
            {
                if (noticia.Enlace.Contains(dominio) == false)
                {
                    enlace = dominio + noticia.Enlace;
                }
                else
                {
                    enlace = noticia.Enlace;
                }
            }
            else
            {
                enlace = dominio + "/news/" + noticia.Id.ToString();
            }

            return enlace;
        }

		public static void ClickTodo(object sender, RoutedEventArgs e)
		{
			Button boton = (Button)sender;
			Noticia noticia = (Noticia)boton.Tag;

			try
			{
				RedesSociales.Discord.Enviar(noticia, RedesSociales.Idiomas.Ingles);
				RedesSociales.Discord.Enviar(noticia, RedesSociales.Idiomas.Español);
			}
			catch
			{
				Herramientas.Notificaciones.Toast("Fallo Discord");
			}

			try
			{
				RedesSociales.Telegram.Enviar(noticia);
			}
			catch
			{
				Herramientas.Notificaciones.Toast("Fallo Telegram");
			}

			try
			{
				RedesSociales.Reddit.Enviar(noticia);
			}
			catch
			{
				Herramientas.Notificaciones.Toast("Fallo Reddit");
			}
		
			try
			{
				RedesSociales.Steam.Enviar(noticia);
			}
			catch 
			{
				Herramientas.Notificaciones.Toast("Fallo Steam");
			}
			
			Pestañas.Visibilidad(ObjetosVentana.gridSteam, true, null, false);
		}

		public static void ClickDiscord(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;
            Noticia noticia = (Noticia)boton.Tag;

            RedesSociales.Discord.Enviar(noticia, RedesSociales.Idiomas.Ingles);
            RedesSociales.Discord.Enviar(noticia, RedesSociales.Idiomas.Español);
        }

		public static void ClickTelegram(object sender, RoutedEventArgs e)
		{
			Button boton = (Button)sender;
			Noticia noticia = (Noticia)boton.Tag;

			RedesSociales.Telegram.Enviar(noticia);
		}

		public static void ClickReddit(object sender, RoutedEventArgs e)
		{
			Button boton = (Button)sender;
			Noticia noticia = (Noticia)boton.Tag;

			RedesSociales.Reddit.Enviar(noticia);
		}

		public static void ClickSteam(object sender, RoutedEventArgs e)
		{
			Button boton = (Button)sender;
			Noticia noticia = (Noticia)boton.Tag;

			RedesSociales.Steam.Enviar(noticia);
			Pestañas.Visibilidad(ObjetosVentana.gridSteam, true, null, false);
		}
	}

    public class Noticia
    {
        public int Id { get; set; }
		public int Tipo { get; set; }
		public int BundleTipo { get; set; }
		public int GratisTipo { get; set; }
		public int SuscripcionTipo { get; set; }
		public string TituloEn { get; set; }
		public string TituloEs { get; set; }
		public string ContenidoEn { get; set; }
		public string ContenidoEs { get; set; }
		public string Imagen { get; set; }
		public string Enlace { get; set; }
		public string Juegos { get; set; }
		public DateTime FechaEmpieza { get; set; }
		public DateTime FechaTermina { get; set; }
		public int BundleId { get; set; }
	}
}

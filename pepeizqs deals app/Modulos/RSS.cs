using Discord;
using FontAwesome6.Fonts;
using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static pepeizqs_deals_app.MainWindow;

namespace Modulos
{
	public static class RSS
	{
		public static void Cargar()
		{
            ObjetosVentana.wvRSS.NavigationCompleted += CompletarCarga;

            ObjetosVentana.wvRSS.Source = new Uri("https://pepeizqdeals.com/news-rss"); 
        }

        private static async void CompletarCarga(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            WebView2 wv = (WebView2)sender;

            if (wv.Source.AbsoluteUri.Contains("https://pepeizqdeals.com/news-rss") == true)
            {
                string html = await wv.CoreWebView2.ExecuteScriptAsync("document.documentElement.outerHTML");

                if (html != null)
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
                            noticias.Reverse();

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

                                #region Discord
                                FontAwesome icono = new FontAwesome
                                {
                                    Icon = FontAwesome6.EFontAwesomeIcon.Brands_Discord,
                                    Foreground = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["ColorFuente"])
                                };

                                Button2 botonDiscord = new Button2
                                {
                                    Tag = noticia,
                                    Content = icono,
                                    Margin = new Thickness(20, 0, 0, 0),
                                    Background = new SolidColorBrush((Windows.UI.Color)Application.Current.Resources["ColorPrimario"])
                                };

                                botonDiscord.Click += ClickDiscord;
                                botonDiscord.PointerEntered += Animaciones.EntraRatonBoton2;
                                botonDiscord.PointerExited += Animaciones.SaleRatonBoton2;

                                sp.Children.Add(botonDiscord);
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

        public static void ClickDiscord(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;
            Noticia noticia = (Noticia)boton.Tag;

            RedesSociales.Discord.Enviar(noticia, RedesSociales.Idiomas.Ingles);
            RedesSociales.Discord.Enviar(noticia, RedesSociales.Idiomas.Español);
        }
    }

    public class Noticia
    {
        public int Id;
        public int Tipo;
        public int BundleTipo;
        public int GratisTipo;
        public int SuscripcionTipo;
        public string TituloEn;
        public string TituloEs;
        public string ContenidoEn;
        public string ContenidoEs;
        public string Imagen;
        public string Enlace;
        public string Juegos;
        public DateTime FechaEmpieza;
        public DateTime FechaTermina;
    }
}

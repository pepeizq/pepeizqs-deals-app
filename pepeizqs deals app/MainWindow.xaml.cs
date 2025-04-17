using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Modulos;
using RedesSociales;

namespace pepeizqs_deals_app
{
	public sealed partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();

			CargarObjetosVentana();

			BarraTitulo.Generar(this);
			BarraTitulo.CambiarTitulo(null);
			Pestañas.Cargar();
			ScrollViewers.Cargar();

			Web.Cargar();
			Humble.Cargar();
			Steam.Cargar();
			Reddit.Cargar();

			Pestañas.Visibilidad(gridWeb, true, null, false);
		}

		public void CargarObjetosVentana()
		{
			ObjetosVentana.ventana = ventana;
			ObjetosVentana.gridTitulo = gridTitulo;
			ObjetosVentana.tbTitulo = tbTitulo;
			ObjetosVentana.nvPrincipal = nvPrincipal;
			ObjetosVentana.nvItemMenu = nvItemMenu;
			ObjetosVentana.menuItemMenu = menuItemMenu;
			ObjetosVentana.nvItemSubirArriba = nvItemSubirArriba;
			ObjetosVentana.nvItemOpciones = nvItemOpciones;

			//-----------------------------------------------------------------

			ObjetosVentana.gridWeb = gridWeb;
			ObjetosVentana.svWeb = svWeb;
			ObjetosVentana.wvWeb = wvWeb;

			//-----------------------------------------------------------------

			ObjetosVentana.gridHumble = gridHumble;
			ObjetosVentana.svHumble = svHumble;
			ObjetosVentana.botonHumbleArrancar = botonHumbleArrancar;
			ObjetosVentana.tbHumblePaginas = tbHumblePaginas;
			ObjetosVentana.wvHumbleAPI = wvHumbleAPI;
			ObjetosVentana.wvHumbleWeb = wvHumbleWeb;

			//-----------------------------------------------------------------

			ObjetosVentana.gridRSS = gridRSS;
			ObjetosVentana.svRSS = svRSS;
            ObjetosVentana.wvRSS = wvRSS;
            ObjetosVentana.spRSSNoticias = spRSSNoticias;

			//-----------------------------------------------------------------

			ObjetosVentana.gridSteam = gridSteam;
			ObjetosVentana.svSteam = svSteam;
			ObjetosVentana.tbSteamEnlace = tbSteamEnlace;
			ObjetosVentana.wvSteam = wvSteam;

			//-----------------------------------------------------------------

			ObjetosVentana.gridReddit = gridReddit;
			ObjetosVentana.svReddit = svReddit;
			ObjetosVentana.botonRedditArrancar = botonRedditArrancar;
			ObjetosVentana.botonRedditSumar = botonRedditSumar;
			ObjetosVentana.tbRedditEnlace = tbRedditEnlace;
			ObjetosVentana.wvReddit = wvReddit;

			//-----------------------------------------------------------------

			ObjetosVentana.gridOpciones = gridOpciones;
			ObjetosVentana.svOpciones = svOpciones;
		}

		public static class ObjetosVentana
		{
			public static Window ventana { get; set; }
			public static Grid gridTitulo { get; set; }
			public static TextBlock tbTitulo { get; set; }
			public static NavigationView nvPrincipal { get; set; }
			public static NavigationViewItem nvItemMenu { get; set; }
			public static MenuFlyout menuItemMenu { get; set; }
			public static NavigationViewItem nvItemSubirArriba { get; set; }
			public static NavigationViewItem nvItemOpciones { get; set; }

			//-----------------------------------------------------------------

			public static Grid gridWeb { get; set; }
			public static ScrollViewer svWeb { get; set; }
			public static WebView2 wvWeb { get; set; }

			//-----------------------------------------------------------------

			public static Grid gridHumble { get; set; }
			public static ScrollViewer svHumble { get; set; }
			public static Button botonHumbleArrancar { get; set; }
			public static TextBlock tbHumblePaginas { get; set; }
            public static WebView2 wvHumbleAPI { get; set; }
			public static WebView2 wvHumbleWeb { get; set; }

			//-----------------------------------------------------------------

			public static Grid gridRSS { get; set; }
			public static ScrollViewer svRSS { get; set; }
            public static WebView2 wvRSS { get; set; }
            public static StackPanel spRSSNoticias { get; set; }

			//-----------------------------------------------------------------

			public static Grid gridSteam { get; set; }
			public static ScrollViewer svSteam { get; set; }
			public static TextBox tbSteamEnlace { get; set; }
			public static WebView2 wvSteam { get; set; }

			//-----------------------------------------------------------------

			public static Grid gridReddit { get; set; }
			public static ScrollViewer svReddit { get; set; }
			public static Button botonRedditArrancar { get; set; }
			public static Button botonRedditSumar { get; set; }
			public static TextBox tbRedditEnlace { get; set; }
			public static WebView2 wvReddit { get; set; }

			//-----------------------------------------------------------------

			public static Grid gridOpciones { get; set; }
			public static ScrollViewer svOpciones { get; set; }
		}

		private void nvPrincipal_Loaded(object sender, RoutedEventArgs e)
		{
			Pestañas.CreadorItems("Reddit");
			Pestañas.CreadorItems("Steam");
			Pestañas.CreadorItems("RSS");
			Pestañas.CreadorItems("Humble");
			Pestañas.CreadorItems("Web");
		}

		private void nvPrincipal_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
			if (args.InvokedItemContainer != null)
			{
				if (args.InvokedItemContainer.GetType() == typeof(NavigationViewItem2))
				{
					NavigationViewItem2 item = args.InvokedItemContainer as NavigationViewItem2;

					if (item.Name == "nvItemMenu")
					{

					}
					else if (item.Name == "nvItemOpciones")
					{
						Pestañas.Visibilidad(gridOpciones, true, null, false);
					}
				}
			}

			if (args.InvokedItem != null)
			{
				if (args.InvokedItem.GetType() == typeof(StackPanel2))
				{
					StackPanel2 sp = (StackPanel2)args.InvokedItem;

					if (sp.Children[0] != null)
					{
						if (sp.Children[0].GetType() == typeof(TextBlock))
						{
							TextBlock tb = sp.Children[0] as TextBlock;

							if (tb.Text == "Web")
							{
								Web.Cargar();
								Pestañas.Visibilidad(gridWeb, true, null, false);
							}
							else if (tb.Text == "Humble")
							{
								Pestañas.Visibilidad(gridHumble, true, null, false);
							}
							else if (tb.Text == "RSS")
							{
                                RSS.Cargar();
                                Pestañas.Visibilidad(gridRSS, true, null, false);
							}
							else if (tb.Text == "Steam")
							{
								Pestañas.Visibilidad(gridSteam, true, null, false);
							}
							else if (tb.Text == "Reddit")
							{
								Pestañas.Visibilidad(gridReddit, true, null, false);
							}
						}
					}
				}
			}
		}	
	}
}

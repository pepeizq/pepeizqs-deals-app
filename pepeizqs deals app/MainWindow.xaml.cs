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
			Pesta�as.Cargar();
			ScrollViewers.Cargar();

			Humble.Cargar();
			Epic.Cargar();
			AmazonLuna.Cargar();
			Steam.Cargar();

			Pesta�as.Visibilidad(gridHumble, true, null, false);
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

			ObjetosVentana.gridHumble = gridHumble;
			ObjetosVentana.svHumble = svHumble;
			ObjetosVentana.botonHumbleArrancar = botonHumbleArrancar;
			ObjetosVentana.tbHumblePaginas = tbHumblePaginas;
			ObjetosVentana.wvHumbleAPI = wvHumbleAPI;

			//-----------------------------------------------------------------

			ObjetosVentana.gridEpic = gridEpic;
			ObjetosVentana.svEpic = svEpic;
			ObjetosVentana.botonEpicArrancar = botonEpicArrancar;
			ObjetosVentana.tbEpicPaginas = tbEpicPaginas;
			ObjetosVentana.wvEpicAPI = wvEpicAPI;

			//-----------------------------------------------------------------

			ObjetosVentana.gridAmazonLuna = gridAmazonLuna;
			ObjetosVentana.svAmazonLuna = svAmazonLuna;
			ObjetosVentana.botonAmazonLunaArrancar = botonAmazonLunaArrancar;
			ObjetosVentana.tbAmazonLuna = tbAmazonLuna;
			ObjetosVentana.wvAmazonLuna = wvAmazonLuna;

			//-----------------------------------------------------------------

			ObjetosVentana.gridRSS = gridRSS;
			ObjetosVentana.svRSS = svRSS;
            ObjetosVentana.spRSSNoticias = spRSSNoticias;

			//-----------------------------------------------------------------

			ObjetosVentana.gridSteam = gridSteam;
			ObjetosVentana.svSteam = svSteam;
			ObjetosVentana.tbSteamEnlace = tbSteamEnlace;
			ObjetosVentana.wvSteam = wvSteam;

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

			public static Grid gridHumble { get; set; }
			public static ScrollViewer svHumble { get; set; }
			public static Button botonHumbleArrancar { get; set; }
			public static TextBlock tbHumblePaginas { get; set; }
            public static WebView2 wvHumbleAPI { get; set; }

			//-----------------------------------------------------------------

			public static Grid gridEpic { get; set; }
			public static ScrollViewer svEpic { get; set; }
			public static Button botonEpicArrancar { get; set; }
			public static TextBlock tbEpicPaginas { get; set; }
			public static WebView2 wvEpicAPI { get; set; }

			//-----------------------------------------------------------------

			public static Grid gridAmazonLuna { get; set; }
			public static ScrollViewer svAmazonLuna { get; set; }
			public static Button botonAmazonLunaArrancar { get; set; }
			public static TextBlock tbAmazonLuna { get; set; }
			public static WebView2 wvAmazonLuna { get; set; }

			//-----------------------------------------------------------------

			public static Grid gridRSS { get; set; }
			public static ScrollViewer svRSS { get; set; }
            public static StackPanel spRSSNoticias { get; set; }

			//-----------------------------------------------------------------

			public static Grid gridSteam { get; set; }
			public static ScrollViewer svSteam { get; set; }
			public static TextBox tbSteamEnlace { get; set; }
			public static WebView2 wvSteam { get; set; }

			//-----------------------------------------------------------------

			public static Grid gridOpciones { get; set; }
			public static ScrollViewer svOpciones { get; set; }
		}

		private void nvPrincipal_Loaded(object sender, RoutedEventArgs e)
		{
			Pesta�as.CreadorItems("Steam");
			Pesta�as.CreadorItems("RSS");
			Pesta�as.CreadorItems("Amazon Luna");
			Pesta�as.CreadorItems("Epic Games");
			Pesta�as.CreadorItems("Humble");
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
						Pesta�as.Visibilidad(gridOpciones, true, null, false);
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

							if (tb.Text == "Humble")
							{
								Pesta�as.Visibilidad(gridHumble, true, null, false);
							}
							else if (tb.Text == "Epic Games")
							{
								Pesta�as.Visibilidad(gridEpic, true, null, false);
							}
							else if (tb.Text == "Amazon Luna")
							{
								Pesta�as.Visibilidad(gridAmazonLuna, true, null, false);
							}
							else if (tb.Text == "RSS")
							{
                                RSS.Cargar();
                                Pesta�as.Visibilidad(gridRSS, true, null, false);
							}
							else if (tb.Text == "Steam")
							{
								Pesta�as.Visibilidad(gridSteam, true, null, false);
							}
						}
					}
				}
			}
		}	
	}
}

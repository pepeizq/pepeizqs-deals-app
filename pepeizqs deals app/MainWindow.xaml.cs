using Interfaz;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Modulos;

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
            ObjetosVentana.wvHumble = wvHumble;
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

            public static WebView2 wvHumble { get; set; }
		}

		private void nvPrincipal_Loaded(object sender, RoutedEventArgs e)
		{
			Pestañas.CreadorItems("Humble");
			Pestañas.CreadorItems("Web");
			
		}

		private void nvPrincipal_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		{
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
								Pestañas.Visibilidad(gridWeb, true, null, false);
							}
							else if (tb.Text == "Humble")
							{
								Pestañas.Visibilidad(gridHumble, true, null, false);
							}
						}
					}
				}
			}
		}
		
	}
}

using Modulos;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RedesSociales
{
	public static class Telegram
	{
		public static async void Enviar(Noticia noticia)
		{
			TelegramBotClient cliente = new TelegramBotClient(DatosPersonales.TelegramToken);

			string enlace = RSS.BuscarEnlace(noticia);

			if (noticia.Imagen != null)
			{
				await cliente.SendPhotoAsync("@pepeizqdeals2", InputFile.FromUri(WebUtility.HtmlDecode(noticia.Imagen)), 0, noticia.TituloEn + " " + enlace);
			}
			else
			{
				await cliente.SendTextMessageAsync("@pepeizqdeals2", noticia.TituloEn + " " + enlace);
			}
			
		}
	}
}

using Modulos;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RedesSociales
{
	public static class Telegram
	{
		public static async void Enviar(Noticia noticia)
		{
			TelegramBotClient cliente = new TelegramBotClient("5558550271:AAFc3Rdwo9AN_1aHDL8ODpi8jaLUd0tSj7Y");

			string enlace = RSS.BuscarEnlace(noticia);

			if (noticia.Imagen != null)
			{
				await cliente.SendPhotoAsync("@pepeizqdeals2", InputFile.FromUri(noticia.Imagen), 0, noticia.TituloEn + " " + enlace);
			}
			else
			{
				await cliente.SendTextMessageAsync("@pepeizqdeals2", noticia.TituloEn + " " + enlace);
			}
			
		}
	}
}

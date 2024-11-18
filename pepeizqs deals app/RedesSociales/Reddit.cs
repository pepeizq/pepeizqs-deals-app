using Modulos;

namespace RedesSociales
{
    public static class Reddit
    {
        public static void Enviar(Noticia noticia)
        {
            RedditSharp.Reddit cliente = new RedditSharp.Reddit();
            cliente.LogIn(DatosPersonales.RedditLogin, DatosPersonales.RedditContraseña);

			cliente.InitOrUpdateUser();

			if (cliente.User != null)
			{
				RedditSharp.Things.Subreddit sub = cliente.GetSubreddit("/r/pepeizqdeals");
				string enlace = RSS.BuscarEnlace(noticia);

				sub.SubmitPost(noticia.TituloEn, enlace);
			}
		}
	}
}

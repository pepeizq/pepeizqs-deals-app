using Discord;
using Discord.Webhook;
using Modulos;
using System;
using System.Collections.Generic;
using System.Net;

namespace RedesSociales
{
    public enum Idiomas
    {
        Ingles,
        Español
    }

    public static class Discord
    {
        public static async void Enviar(Noticia noticia, Idiomas idioma)
        {
            string hook = null;

            if (idioma == Idiomas.Ingles) 
            {
                hook = DatosPersonales.DiscordIngles;
            }
            else if (idioma == Idiomas.Español)
            {
                hook = DatosPersonales.DiscordEspañol;
            }

            if (hook != null)
            {
                using (DiscordWebhookClient cliente = new DiscordWebhookClient(hook))
                {
                    EmbedBuilder constructor = new EmbedBuilder();

                    if (idioma == Idiomas.Ingles)
                    {
                        constructor.Title = noticia.TituloEn;
                    }
                    else if (idioma == Idiomas.Español)
                    {
                        constructor.Title = noticia.TituloEs;
                    }

                    string enlace = RSS.BuscarEnlace(noticia);

                    constructor.Url = enlace;

                    if (noticia.Imagen != null)
                    {
                        constructor.ImageUrl = WebUtility.HtmlDecode(noticia.Imagen);
                    }

                    List<Embed> lista = new List<Embed>
                    {
                        constructor.Build()
                    };

                    if (idioma == Idiomas.Ingles)
                    {
                        await cliente.SendMessageAsync(noticia.TituloEn + Environment.NewLine + enlace, false, lista, "pepebot5");
                    }
                    else if (idioma == Idiomas.Español)
                    {
                        await cliente.SendMessageAsync(noticia.TituloEs + Environment.NewLine + enlace, false, lista, "pepebot5");
                    }
                }
            }    
        }
    }
}

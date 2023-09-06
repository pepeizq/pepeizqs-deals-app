using Discord;
using Discord.Webhook;
using Modulos;
using System;
using System.Collections.Generic;

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
                hook = "https://discordapp.com/api/webhooks/1148593428771381370/AdstfYvX0m34rDOVLasrU8Ed1ngX9qjfOtcb7v5vq9VPFP2i3G-p5hZxbuw8gvGyDap1";
            }
            else if (idioma == Idiomas.Español)
            {
                hook = "https://discordapp.com/api/webhooks/1148600562846281849/6K4wF8DnzCAO24lihO5u4fXO9YbG4VsjjHcdxYYRItQXE7o2p3x7OEqzAisWWZ3ywTlS";
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
                        constructor.ImageUrl = noticia.Imagen;
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

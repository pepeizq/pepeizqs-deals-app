using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using Modulos;
using System;
using System.Threading.Tasks;
using static pepeizqs_deals_app.MainWindow;

namespace RedesSociales
{
    public static class Steam
    {
		public static Noticia noticia { get; set; }

        public static void Cargar()
        {
			ObjetosVentana.wvSteam.CoreWebView2Initialized += InyectarDetectorUrl;
			ObjetosVentana.wvSteam.NavigationCompleted += CompletarCarga;
		}

		private static async void CompletarCarga(object sender, object e)
        {
            WebView2 wv = (WebView2)sender;

			ObjetosVentana.tbSteamEnlace.Text = wv.Source.AbsoluteUri;

			if (wv.Source == new Uri("https://steamcommunity.com/groups/pepedeals/partnerevents/create") && noticia != null)
			{
				await Task.Delay(2000);
				await wv.ExecuteScriptAsync(@"document.querySelector('._48mLX0pHw-bU9oIl-5dGm').click();");
			}
		}

		public static  void Enviar(Noticia noticia2)
		{
			noticia = noticia2;
			ObjetosVentana.wvSteam.Source = new Uri("https://steamcommunity.com/groups/pepedeals/partnerevents/create");
		}

		private static async void InyectarDetectorUrl(WebView2 sender, CoreWebView2InitializedEventArgs args)
		{
			await sender.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(@"
				(function() {
					const observer = (url) => {
						window.chrome.webview.postMessage('URL_CHANGED:' + url);
					};

					const pushState = history.pushState.bind(history);
					history.pushState = function(state, title, url) {
						pushState(state, title, url);
						observer(location.href);
					};

					const replaceState = history.replaceState.bind(history);
					history.replaceState = function(state, title, url) {
						replaceState(state, title, url);
						observer(location.href);
					};

					window.addEventListener('popstate', () => observer(location.href));
				})();
			");

			sender.CoreWebView2.WebMessageReceived += MensajeRecibido;
		}

		private static async void MensajeRecibido(object sender, CoreWebView2WebMessageReceivedEventArgs e)
		{
			string mensaje = e.TryGetWebMessageAsString();

			if (mensaje.StartsWith("URL_CHANGED:"))
			{
				string nuevaUrl = mensaje.Replace("URL_CHANGED:", "");
				ObjetosVentana.tbSteamEnlace.Text = nuevaUrl;

				WebView2 wv = ObjetosVentana.wvSteam;

				if (nuevaUrl.Contains("/partnerevents/edit/"))
				{
					await Task.Delay(2000);

					#region Ingles

					await wv.ExecuteScriptAsync(@"
						(function() {
							const select = document.querySelector('.tool-tip-source select');
							const option = Array.from(select.options).find(o => o.text === 'English');
							if (!option) return;
							const setter = Object.getOwnPropertyDescriptor(window.HTMLSelectElement.prototype, 'value').set;
							setter.call(select, option.value);
							select.dispatchEvent(new Event('change', { bubbles: true }));
						})();
					");

					await wv.ExecuteScriptAsync(@"
						(function() {
							const input = document.querySelector('.ZAOXnBPqM-Jpp-1EVcR39');
							if (!input) return;
							const setter = Object.getOwnPropertyDescriptor(window.HTMLInputElement.prototype, 'value').set;
							setter.call(input, '" + noticia.SteamEn.Titulo + @"');
							input.dispatchEvent(new Event('input', { bubbles: true }));
							input.dispatchEvent(new Event('change', { bubbles: true }));
						})();
					");

					await wv.ExecuteScriptAsync(@"
						(function() {
							const input = document.querySelector('._32XZf0gEFw0CwBBwiLjlEv');
							if (!input) return;
							const setter = Object.getOwnPropertyDescriptor(window.HTMLInputElement.prototype, 'value').set;
							setter.call(input, '" + noticia.SteamEn.Subtitulo + @"');
							input.dispatchEvent(new Event('input', { bubbles: true }));
							input.dispatchEvent(new Event('change', { bubbles: true }));
						})();
					");

					await wv.ExecuteScriptAsync(@"
						(function() {
							const labels = Array.from(document.querySelectorAll('label'));
							const label = labels.find(l => l.textContent.trim().includes('Use visual editor'));
							if (!label) return;
							const checkbox = label.querySelector('input[type=""checkbox""]');
							if (!checkbox) return;

							if (checkbox.checked) {
								label.click(); // Clic sobre el label, como haría un usuario real
							}
						})();
					");

					await Task.Delay(100);
					string contenidoEscapadoEn = System.Text.Json.JsonSerializer.Serialize(noticia.SteamEn.Contenido);

					await wv.ExecuteScriptAsync($@"
						(function() {{
							const contenido = {contenidoEscapadoEn};
							const textarea = document.querySelector('textarea[placeholder=""Enter Event Description here""]');
							if (!textarea) return;

							textarea.focus();

							const setter = Object.getOwnPropertyDescriptor(window.HTMLTextAreaElement.prototype, 'value').set;
							setter.call(textarea, contenido);

							textarea.dispatchEvent(new Event('focus', {{ bubbles: true }}));
							textarea.dispatchEvent(new Event('input', {{ bubbles: true }}));
							textarea.dispatchEvent(new Event('change', {{ bubbles: true }}));
							textarea.dispatchEvent(new Event('blur', {{ bubbles: true }}));
						}})();
					");

					#endregion

					#region Español

					await Task.Delay(3000);
					await wv.ExecuteScriptAsync(@"
						(function() {
							const select = document.querySelector('.tool-tip-source select');
							if (!select) return;
							const option = Array.from(select.options).find(o => o.text === 'Spanish - Spain');
							if (!option) return;
							const setter = Object.getOwnPropertyDescriptor(window.HTMLSelectElement.prototype, 'value').set;
							setter.call(select, option.value);
							select.dispatchEvent(new Event('change', { bubbles: true }));
						})();
					");

					await wv.ExecuteScriptAsync(@"
						(function() {
							const input = document.querySelector('.ZAOXnBPqM-Jpp-1EVcR39');
							if (!input) return;
							const setter = Object.getOwnPropertyDescriptor(window.HTMLInputElement.prototype, 'value').set;
							setter.call(input, '" + noticia.SteamEs.Titulo + @"');
							input.dispatchEvent(new Event('input', { bubbles: true }));
							input.dispatchEvent(new Event('change', { bubbles: true }));
						})();
					");

					await wv.ExecuteScriptAsync(@"
						(function() {
							const input = document.querySelector('._32XZf0gEFw0CwBBwiLjlEv');
							if (!input) return;
							const setter = Object.getOwnPropertyDescriptor(window.HTMLInputElement.prototype, 'value').set;
							setter.call(input, '" + noticia.SteamEs.Subtitulo + @"');
							input.dispatchEvent(new Event('input', { bubbles: true }));
							input.dispatchEvent(new Event('change', { bubbles: true }));
						})();
					");

					await wv.ExecuteScriptAsync(@"
						(function() {
							const labels = Array.from(document.querySelectorAll('label'));
							const label = labels.find(l => l.textContent.trim().includes('Use visual editor'));
							if (!label) return;
							const checkbox = label.querySelector('input[type=""checkbox""]');
							if (!checkbox) return;

							if (checkbox.checked) {
								label.click(); // Clic sobre el label, como haría un usuario real
							}
						})();
					");

					await Task.Delay(100);
					string contenidoEscapadoEs = System.Text.Json.JsonSerializer.Serialize(noticia.SteamEs.Contenido);

					await wv.ExecuteScriptAsync($@"
						(function() {{
							const contenido = {contenidoEscapadoEs};
							const textarea = document.querySelector('textarea[placeholder=""Enter Event Description here""]');
							if (!textarea) return;

							textarea.focus();

							const setter = Object.getOwnPropertyDescriptor(window.HTMLTextAreaElement.prototype, 'value').set;
							setter.call(textarea, contenido);

							textarea.dispatchEvent(new Event('focus', {{ bubbles: true }}));
							textarea.dispatchEvent(new Event('input', {{ bubbles: true }}));
							textarea.dispatchEvent(new Event('change', {{ bubbles: true }}));
							textarea.dispatchEvent(new Event('blur', {{ bubbles: true }}));
						}})();
					");

					#endregion

					await Task.Delay(1000);
					await wv.ExecuteScriptAsync(@"
						(function() {
							const btn = Array.from(document.querySelectorAll('[role=""button""]'))
								.find(b => b.textContent.trim().startsWith('Save'));
							if (!btn) return;
							btn.click();
						})();
					");

					await Task.Delay(1000);
					await wv.ExecuteScriptAsync(@"
						(function() {
							const link = Array.from(document.querySelectorAll('a'))
								.find(a => a.textContent.trim() === 'Preview Announcement');
							if (!link) return;
							link.click();
						})();
					");

					await Task.Delay(1000);
					await wv.ExecuteScriptAsync(@"
						(function() {
							const link = Array.from(document.querySelectorAll('a'))
								.find(a => a.textContent.trim() === 'Publish');
							if (!link) return;
							link.click();
						})();
					");

					await Task.Delay(1000);
					await wv.ExecuteScriptAsync(@"
						(function() {
							const btn = Array.from(document.querySelectorAll('[role=""button""]'))
								.find(b => b.textContent.trim().startsWith('Skip Warnings and Continue Publishing...'));
							if (!btn) return;
							btn.click();
						})();
					");

					await Task.Delay(1000);
					await wv.ExecuteScriptAsync(@"
						(function() {
							const btn = Array.from(document.querySelectorAll('[role=""button""]'))
								.find(b => b.textContent.trim().startsWith('Publish'));
							if (!btn) return;
							btn.click();
						})();
					");
				}
			}
		}
	}
}

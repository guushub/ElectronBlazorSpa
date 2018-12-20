using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net.Mime;

namespace ElectronBlazorSpa.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });

            app.UseBlazor<Client.Startup>();

            Bootstrap();
        }

        public async void Bootstrap()
        {
            var options = new BrowserWindowOptions
            {
                Show = false
            };
            var mainWindow = await Electron.WindowManager.CreateWindowAsync(options);
            mainWindow.OnReadyToShow += () =>
            {
                mainWindow.Show();
            };

            var menu = new MenuItem[]
            {
                new MenuItem
                {
                    Label = "File",
                    Submenu = new MenuItem[]
                    {
                        new MenuItem {
                            Label = "Exit",
                            Click = () => { Electron.App.Exit(); }
                        }
                    }
                },
                new MenuItem
                {
                    Label = "About",
                    Click = () =>
                    {
                        Electron.Dialog.ShowMessageBoxAsync(String.Format("Dit is dus met Electron.Net en Blazor gemaakt! Killer combo :-). Wel nog wat experimenteel, maar binnenkort komt de officiele versie van Blazor uit. Voordeel is in ieder geval dat 'wij' als WebDevelopers makkelijker een desktop applicatie kunnen maken.{0}{0}Voor meer info, zie https://github.com/ElectronNET/Electron.NET en https://blazor.net", Environment.NewLine));
                    }
                }
            };

            Electron.Menu.SetApplicationMenu(menu);
        }
    }
}

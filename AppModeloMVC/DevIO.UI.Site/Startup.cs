using DevIO.UI.Site.Data;
using DevIO.UI.Site.Servicos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DevIO.UI.Site
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            services.AddMvc();

            // Transient: Obt�m uma nova inst�ncia do objeto a cada solicita��o
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IOperacaoTransient, Operacao>(); // Transient: Obt�m uma nova inst�ncia do objeto a cada solicita��o

            // Scoped: Reutiliza a mesma inst�ncia do objeto durante todo o request (web)
            services.AddScoped<IOperacaoScoped, Operacao>();
            
            // Singleton: Utiliza a mesma inst�ncia para toda a aplica��o (cuidado)
            services.AddSingleton<IOperacaoSingleton, Operacao>();
            services.AddSingleton<IOperacaoSingletonInstance>(new Operacao(Guid.Empty));

            services.AddTransient<OperacaoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute("AreaProdutos","Produtos", "{Produtos}/{controller=Cadastro}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute("AreaVendas", "Vendas", "{Vendas}/{controller=Pedidos}/{action=Index}/{id?}");
            });
        }
    }
}
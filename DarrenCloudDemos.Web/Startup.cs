using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoWrapper;
using DarrenCloudDemos.Lib.Helpers;
using DarrenCloudDemos.Web.Extensions;
using Microsoft.Extensions.Primitives;

namespace DarrenCloudDemos.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
             IServiceCollection用来配置依赖容器中的服务。然后被Buld成IServiceProvider,IServiceProvider可看作时容器。

            IApplicationBuilder.ApplicationServices返回的是IServiceProvider

            HttpContext.RequestServices返回的是IServiceProvider

            如何获取服务呢？

            var service  = (IFooService)serviceProvider.GetService(typeof(IFooService));

            如果引用Microsoft.Extensions.DependencyInjection，可以这样获取服务：

            var service  = serviceProvider.GetService<IFooService>();


             */


            services.AddControllersWithViews();

            #region 加密

            services.AddCertificateManager();
            services.AddTransient<SymmetricEncryptDecrypt>();
            services.AddTransient<AsymmetricEncryptDecrypt>();
            services.AddTransient<DigitalSignatures>(); 
            #endregion

            services.Configure<App>(Configuration.GetSection(nameof(App)));//把appsettings.json中的配置读取到App


            //services.ConfigureDictionary<SourceSetting>(Configuration.GetSection(nameof(SourceSetting)));//配置字典集，通常写法


            var sourceSetting = new SourceSetting();
            Configuration.GetSection("SourceSetting").Bind(sourceSetting);
            services.AddSingleton(sourceSetting);
            Action onChange = () =>
            {
                Configuration.GetSection("SourceSetting").Bind(sourceSetting);
                Console.WriteLine("onChange fired!");
            };

            ChangeToken.OnChange(() => Configuration.GetReloadToken(), onChange);


            ////通过Bind把类和配置文件绑定起来
            //var leaningOptions = new LearningOptions();
            //Configuration.GetSection("Learning").Bind(leaningOptions);

            ////获取配置文件对应的类实例
            //leaningOptions = Configuration.GetSection("Learning").Get<LearningOptions>();

            ////通过Configure把类和配置文件绑定起来
            //services.Configure<LearningOptions>(Configuration.GetSection("Learning"));

            ////通过PostConfigure把类和配置文件绑定起来，并且可以附加动作
            //services.PostConfigure<LearningOptions>(options => options.Years += 1);

            ////通过委托设置
            //services.Configure<AppInfoOptions>(options =>
            //{
            //    options.AppName = "ASP.NET Core";
            //    options.AppVersion = "1.2.1";
            //});

            //services.Configure<ThemeOptions>("DarkTheme", Configuration.GetSection("Themes:Dark"));
            //services.Configure<ThemeOptions>("WhiteTheme", Configuration.GetSection("Themes:White"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            ServiceActivator.Configure(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseApiResponseAndExceptionWrapper();//统一API响应和异常处理

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

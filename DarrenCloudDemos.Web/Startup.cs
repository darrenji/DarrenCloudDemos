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
using DarrenCloudDemos.Lib.Threads;
using DarrenCloudDemos.Lib;
using Microsoft.AspNetCore.Diagnostics;
using DarrenCloudDemos.Lib.Exceptions;
using Microsoft.AspNetCore.Mvc;
using DarrenCloudDemos.Web.Models;

namespace DarrenCloudDemos.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
             IServiceCollection�����������������еķ���Ȼ��Buld��IServiceProvider,IServiceProvider�ɿ���ʱ������

            IApplicationBuilder.ApplicationServices���ص���IServiceProvider

            HttpContext.RequestServices���ص���IServiceProvider

            ��λ�ȡ�����أ�

            var service  = (IFooService)serviceProvider.GetService(typeof(IFooService));

            �������Microsoft.Extensions.DependencyInjection������������ȡ����

            var service  = serviceProvider.GetService<IFooService>();


             */


            services.AddControllersWithViews();

            #region ����

            services.AddCertificateManager();
            services.AddTransient<SymmetricEncryptDecrypt>();
            services.AddTransient<AsymmetricEncryptDecrypt>();
            services.AddTransient<DigitalSignatures>();
            #endregion

            #region ����
            services.Configure<App>(Configuration.GetSection(nameof(App)));//��appsettings.json�е����ö�ȡ��App


            //services.ConfigureDictionary<SourceSetting>(Configuration.GetSection(nameof(SourceSetting)));//�����ֵ伯��ͨ��д��


            var sourceSetting = new SourceSetting();
            Configuration.GetSection("SourceSetting").Bind(sourceSetting);
            services.AddSingleton(sourceSetting);
            Action onChange = () =>
            {
                Configuration.GetSection("SourceSetting").Bind(sourceSetting);
                Console.WriteLine("onChange fired!");
            };

            ChangeToken.OnChange(() => Configuration.GetReloadToken(), onChange);


            ////ͨ��Bind����������ļ�������
            //var leaningOptions = new LearningOptions();
            //Configuration.GetSection("Learning").Bind(leaningOptions);

            ////��ȡ�����ļ���Ӧ����ʵ��
            //leaningOptions = Configuration.GetSection("Learning").Get<LearningOptions>();

            ////ͨ��Configure����������ļ�������
            //services.Configure<LearningOptions>(Configuration.GetSection("Learning"));

            ////ͨ��PostConfigure����������ļ������������ҿ��Ը��Ӷ���
            //services.PostConfigure<LearningOptions>(options => options.Years += 1);

            ////ͨ��ί������
            //services.Configure<AppInfoOptions>(options =>
            //{
            //    options.AppName = "ASP.NET Core";
            //    options.AppVersion = "1.2.1";
            //});

            //services.Configure<ThemeOptions>("DarkTheme", Configuration.GetSection("Themes:Dark"));
            //services.Configure<ThemeOptions>("WhiteTheme", Configuration.GetSection("Themes:White")); 
            #endregion

            #region �̺߳͵��Ժ���־
            ThreadMananger.RegisterThreads();
            var ddSetting = new DDSetting();
            Configuration.GetSection("DDSetting").Bind(ddSetting);
            Config.DDSetting = ddSetting;
            Config.WebPath = Env.ContentRootPath;
            #endregion

            #region AutoWrapper

            services.Configure<ApiBehaviorOptions>(options =>
            {
                //ȡ��APIController��OnActionExecuting�¼���ģ�͵���֤����400 Bad Request
                options.SuppressModelStateInvalidFilter = true;
            }); 
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //ServiceActivator.Configure(app.ApplicationServices);


            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            //app.UseApiExceptionHandler();//APIȫ���쳣����

            app.UseStaticFiles();

            #region AutoWrapper
            //app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { UseApiProblemDetailsException =true});//ͳһAPI��Ӧ���쳣����
            //app.UseApiResponseAndExceptionWrapper();//ͳһAPI��Ӧ���쳣���� 
            //app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { UseCustomSchema = true });//ͳһAPI��Ӧ���쳣���� 
            app.UseApiResponseAndExceptionWrapper<MapResponseObject>(new AutoWrapperOptions { ApiVersion="1.0.0.0", ShowApiVersion=true, ShowStatusCode=true,ShowIsErrorFlagForSuccessfulResponse=true});
            #endregion

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

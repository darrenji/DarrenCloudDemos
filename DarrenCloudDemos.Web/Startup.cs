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
using DarrenCloudDemos.Web.ApiKeys;
using DarrenCloudDemos.Web.ApiKeys.Authentication;
using DarrenCloudDemos.Web.ApiKeys.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DarrenCloudDemos.Web.NormalAuth.Shared;
using DarrenCloudDemos.Web.NormalAuth.Authentication;
using DarrenCloudDemos.Web.NormalAuth.Authorization;
using Microsoft.AspNetCore.Http;
using DarrenCloudDemos.Web.MultiTenants;
using Microsoft.EntityFrameworkCore;
using DarrenCloudDemos.Web.Database;
using Microsoft.OpenApi.Models;
using MediatR;
using System.Reflection;
using FluentValidation;
using DarrenCloudDemos.Web.PipelineBehaviours;

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


            services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

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

            #region API Key ��֤��Ȩ

            /*
             ��֤�����÷����ˣ�AuthenticationSchemeOptions
             ��֤����֤�߼������ˣ�AuthenticationHandler
             AuthenticaitonBuilder�ǽ�����

             ��֤���߼��ǣ�List<Cliam> �� List<ClaimsIdentity> �� ClaimsPrincipal �� AuthenticationTicket
             */

            //services.AddAuthentication(options =>
            //{               
            //    options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            //    options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            //}).AddApiKeySupport(options => { });

            /*
             ��Ȩ
             IAuthorizationRequirement 
             AuthorizationHandler<IAuthorizationRequirement>
             */

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(Policies.OnlyEmployees, policy => policy.Requirements.Add(new OnlyEmployeesRequirement()));
            //    options.AddPolicy(Policies.OnlyManagers, policy => policy.Requirements.Add(new OnlyManagersRequirement()));
            //    options.AddPolicy(Policies.OnlyThirdParties, policy => policy.Requirements.Add(new OnlyThirdPartiesRequirement()));
            //});

            //services.AddSingleton<IAuthorizationHandler, OnlyEmployeesAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationHandler, OnlyManagersAuthorizationHandler>();
            //services.AddSingleton<IAuthorizationHandler, OnlyThirdPartiesAuthorizationHandler>();

            //services.AddSingleton<IGetApiKeyQuery, InMemoryGetApiKeyQuery>();

            //services.AddRouting(x => x.LowercaseUrls = true);
            #endregion

            //����
            services.AddMemoryCache();

            #region �ֻ��ź���֤���¼ע��
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = NormalAuthAuthenticationOptions.DefaultScheme;
                x.DefaultChallengeScheme = NormalAuthAuthenticationOptions.DefaultScheme;
            })
            .AddNormalAuthSupport(options => { })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters();
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(NormalAuthPolicies.OnlyAdmin, policy => policy.Requirements.Add(new OnlyAdminRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, OnlyAdminAuthorizationHandler>();
            services.AddSingleton<TokenHelper>();
            services.AddSingleton<IGetNormalUser, InMemoryGetNormalUser>();
            #endregion

            #region multi-tenant
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // To access HttpContext
            services.AddDbContext<GlobalDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<TenantDbContext>();
            services.AddScoped<DbSeeder>();
            #endregion

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            #region Swagger
            //services.AddSwaggerGen(c =>
            //{
            //    c.IncludeXmlComments(string.Format(@"{0}\CQRS.WebApi.xml", System.AppDomain.CurrentDomain.BaseDirectory));
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Version = "v1",
            //        Title = "CQRS.WebApi",
            //    });

            //});
            #endregion
            #region MediatR
            services.AddScoped<IApplicationContext>(provider => provider.GetService<ApplicationContext>());

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>)); 
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

            #region multi-tenant
            app.UseTenantIdentifier();
            #endregion

            #region AutoWrapper
            //app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { UseApiProblemDetailsException =true});//ͳһAPI��Ӧ���쳣����
            //app.UseApiResponseAndExceptionWrapper();//ͳһAPI��Ӧ���쳣���� 
            //app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { UseCustomSchema = true });//ͳһAPI��Ӧ���쳣���� 
            app.UseApiResponseAndExceptionWrapper<MapResponseObject>(new AutoWrapperOptions { ApiVersion="1.0.0.0", ShowApiVersion=true, ShowStatusCode=true,ShowIsErrorFlagForSuccessfulResponse=true});
            #endregion

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            #region Swagger
            //// Enable middleware to serve generated Swagger as a JSON endpoint.
            //app.UseSwagger();

            //// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            //// specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS.WebApi");
            //});
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

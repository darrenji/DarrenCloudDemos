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
             IServiceCollection用来配置依赖容器中的服务。然后被Buld成IServiceProvider,IServiceProvider可看作时容器。

            IApplicationBuilder.ApplicationServices返回的是IServiceProvider

            HttpContext.RequestServices返回的是IServiceProvider

            如何获取服务呢？

            var service  = (IFooService)serviceProvider.GetService(typeof(IFooService));

            如果引用Microsoft.Extensions.DependencyInjection，可以这样获取服务：

            var service  = serviceProvider.GetService<IFooService>();


             */


            services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            #region 加密

            services.AddCertificateManager();
            services.AddTransient<SymmetricEncryptDecrypt>();
            services.AddTransient<AsymmetricEncryptDecrypt>();
            services.AddTransient<DigitalSignatures>();
            #endregion

            #region 配置
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
            #endregion

            #region 线程和调试和日志
            ThreadMananger.RegisterThreads();
            var ddSetting = new DDSetting();
            Configuration.GetSection("DDSetting").Bind(ddSetting);
            Config.DDSetting = ddSetting;
            Config.WebPath = Env.ContentRootPath;
            #endregion

            #region AutoWrapper

            services.Configure<ApiBehaviorOptions>(options =>
            {
                //取消APIController中OnActionExecuting事件对模型的验证返回400 Bad Request
                options.SuppressModelStateInvalidFilter = true;
            });
            #endregion

            #region API Key 验证授权

            /*
             验证的配置放在了：AuthenticationSchemeOptions
             验证的验证逻辑放在了：AuthenticationHandler
             AuthenticaitonBuilder是建造者

             验证的逻辑是：List<Cliam> → List<ClaimsIdentity> → ClaimsPrincipal → AuthenticationTicket
             */

            //services.AddAuthentication(options =>
            //{               
            //    options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            //    options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            //}).AddApiKeySupport(options => { });

            /*
             授权
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

            //缓存
            services.AddMemoryCache();

            #region 手机号和验证码登录注册
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

            //app.UseApiExceptionHandler();//API全局异常处理

            app.UseStaticFiles();

            #region multi-tenant
            app.UseTenantIdentifier();
            #endregion

            #region AutoWrapper
            //app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { UseApiProblemDetailsException =true});//统一API响应和异常处理
            //app.UseApiResponseAndExceptionWrapper();//统一API响应和异常处理 
            //app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { UseCustomSchema = true });//统一API响应和异常处理 
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

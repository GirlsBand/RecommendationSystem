﻿using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using RecommendationSystem.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecommendationSystem
{
    public class Startup
    {
        const string AppId = "193302211282683";
        const string AppSecret = "2fb8063b7eded7b8a0f63b68c108dc7f";
        
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCorsMiddleware();
            app.UseMvcWithDefaultRoute();

            app.UseAuthentication();

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
                       
            services.AddSingleton<IFacebookService>(new FacebookService(new SocialNetHttpClient("https://graph.facebook.com/v2.12")));
            services.AddSingleton(new DestinationProvider(new HttpClient(), "http://localhost:3210/api/destination/prod"));
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = AppId;
                facebookOptions.AppSecret = AppSecret;
                facebookOptions.Scope.Add("user_tagged_places");
                facebookOptions.Fields.Add("tagged_places");
            })
            .AddCookie();

            services.AddAntiforgery(
                options =>
                {
                    options.Cookie.Name = "_af";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.HeaderName = "X-XSRF-TOKEN";
                }
            );
        }
    }
}
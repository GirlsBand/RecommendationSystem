using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace RecommendationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://*:3001")
                .Build()
                .Run();
        }
    }
}
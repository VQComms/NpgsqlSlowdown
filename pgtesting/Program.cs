using Owin;
using Microsoft.Owin.Hosting;
using Nancy.Owin;
using System;
using Nancy;
using Npgsql;
using System.Configuration;

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        app.UseNancy();
    }
}

class Program
{
    static void Main(string[] args)
    {
        var url = "http://172.16.185.130:8080";

        using (WebApp.Start<Startup>(url))
        {
            Console.WriteLine("Running on {0}", url);
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}

public class RootModule : NancyModule
{
    public RootModule()
    {
        Get["/"] = _ =>
        { 
            using (var connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["VQr"].ConnectionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand("SELECT 1=1", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Console.WriteLine("ooh data");
                        }
                    }
                }    
            }

            return 200;
        };
    }

}
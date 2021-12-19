using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace poc_cosmosdb_1
{
    internal class Service
    {

        private static void Main() {

            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Service>()
                .Build();

            var ConnectionString = builder["ConnectionString"];

            Console.WriteLine($"{builder["ConnectionString"]}");
        }
    }
}

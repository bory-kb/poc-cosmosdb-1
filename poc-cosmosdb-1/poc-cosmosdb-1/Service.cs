using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Cosmos.Fluent;

namespace poc_cosmosdb_1
{
    internal class Service
    {

        private static async Task Main()
        {

            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Service>()
                .Build();

            var ConnectionString = builder["ConnectionString"];

            var cosmosClient = new CosmosClient(ConnectionString, new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            });

            var container = cosmosClient.GetContainer("TodoList", "TodoItem");

            var response = await container.ReadItemAsync<TodoItem>("9c3519b6-8a18-4eca-b034-1045f0b2d7e7",
                new PartitionKey("bory")
                );

            var todoItem = response.Resource;

            Console.WriteLine($"{todoItem.Title},{todoItem.Body},{todoItem.User.Name}");
        }

    }

    public class TodoItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public User User { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
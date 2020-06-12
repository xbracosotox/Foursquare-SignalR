using System;
using Microsoft.AspNetCore.SignalR.Client;


namespace ClientConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            LocationDTO res = new LocationDTO();

            var connection = new HubConnectionBuilder().WithUrl("http://localhost:53781/ChatHub").Build();

            connection.StartAsync().GetAwaiter().GetResult();

            Console.WriteLine("Listening.... ");

            connection.On<LocationDTO>("OnLocation", (task) =>
            {
                Console.WriteLine($"Somebody searched for {task.latitude}, {task.longitude}");
            });
            
            Console.Read();


        }
    }
}

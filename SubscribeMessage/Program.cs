
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;



namespace SubscribeMessage
{
    public class Program:IotMqtt
    {
        static string conString = "broker.hivemq.com";
        string clientId = new Guid().ToString();

        public static void Main(string[] args)
        {
            IotMqtt obj = new IotMqtt();

            Dictionary<string, object> dict = new Dictionary<string, object>();

            Console.WriteLine("Enter topic to subscribe");
            string subsTopic = Console.ReadLine();
            dict.Add("subscribeTopic", subsTopic);


            dict.Add("mqttConnectionString", conString);

            obj.Open(dict);

            Console.ReadKey();





        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using uPLibrary.Networking.M2Mqtt.Messages;
using Daenet.IoTApi;
using Daenet.Iot;
using System.Threading;
using uPLibrary.Networking;
using System.Net;
using System.Text;
using SubscribeMessage;

namespace PublishMesssage
{
    public class Program : IotMqtt
    {
     static string conString = "broker.hivemq.com";
        string clientid = new Guid().ToString();




        public static void Main(string[] args)
        {

            IotMqtt obj = new IotMqtt();

            Dictionary<string, object> dict = new Dictionary<string, object>();

            //Console.WriteLine("Enter topic to subscribe");
            //string subsTopic = Console.ReadLine();                     
            //dict.Add("subscribeTopic", subsTopic);
            Console.WriteLine("Enter the Topic name to publish");
            string topic = Console.ReadLine();
            Dictionary<string, object> dtopic = new Dictionary<string, object>();
            dtopic.Add("topicName", topic);

            dict.Add("mqttConnectionString", conString);

            obj.Open(dict);
            

            try
            {
           

                        

           
                bool repeat = true;


                while (repeat)
                {

                    Console.WriteLine("Write the msg ! :");
                    string msg = Console.ReadLine();

                    List<object> mlist = new List<object>(); 
                    mlist.Add(msg);

                    IList<object> lst = mlist;
                    
                    obj.SendAsync(lst, null, null, dtopic);

                }

                Console.ReadKey();
            }
            catch(Exception ex)
            {
               Console.WriteLine(ex.ToString());
            }



    } }
    }

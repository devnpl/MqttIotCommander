using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


namespace ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {

            MqttClient client = new MqttClient("test.mosquitto.org");
            string uniqueclientid = Guid.NewGuid().ToString();
            client.Connect(uniqueclientid);

            Console.WriteLine("Enter first topic name to subscribe");
            string topic = Console.ReadLine();

            Console.WriteLine("Enter second topic name to subscribe");
            string topic1 = Console.ReadLine();

            string[] TopicArray = new string[] { topic , topic1};
            byte[] QosLabelArray = new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE,MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE };
            client.Subscribe(TopicArray, QosLabelArray);

            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;


            Console.ReadKey();

        }

        private static void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            byte[] respose = e.Message;
            string topicname = e.Topic;
            string byteToString = Encoding.ASCII.GetString(respose);
            Console.WriteLine("Topic name : " + topicname + "  Data :" + byteToString);
        }


    }
}

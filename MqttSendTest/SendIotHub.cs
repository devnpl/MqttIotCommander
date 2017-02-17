using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MqttSendTest
{
    public class SendIotHub
    {
        public static void Main(string[] args)
        {           //Declaration for new client
            MqttClient newmqttClient = new MqttClient("test.mosquitto.org");    
                                            
            //  MqttClient newmqttClient = new MqttClient("HostName=daenethub.azure-devices.net;SharedAccessKey=/xHcc1oTsW9bJb2NFYPTSFZOUM2eMdL0bGgTjxJSkUY=");
           
           // string deviceId1 = "IgusDE023";
           string deviceId1= "IgusDE023";
            // as stated in https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-mqtt-support

            string username1 = "daenethub.azure-devices.net/IgusDE023/api-version=2016-11-14";
          //using device explorer generated SAS token
            string password1 = "SharedAccessSignature sr=daenethub.azure-devices.net&sig=MNpOZAVwv4Idj3l%2B5aktL1X9TgiZ2gUfR2ga8Jx%2FyU4%3D&se=1518784069&skn=mqtttest";
           
            newmqttClient.Connect(deviceId1, username1, password1);

             Console.WriteLine("The device is connected!!!");
            try
            {
                Console.WriteLine("Enter the Topic name to publish");
                string topic = Console.ReadLine();

                bool repeat = true;

                    while (repeat)
                    {
                        Console.WriteLine("Write the msg or 'x' to close :");
                        string msg = Console.ReadLine();

                        if (msg.ToLower().Equals("x"))
                        {
                            repeat = false;
                            break;
                        }

                        newmqttClient.Publish(topic, Encoding.ASCII.GetBytes(msg));
                    }               

                Console.WriteLine("Publish stopped. Please exit the application");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

            Console.ReadKey();

        }
    }

}


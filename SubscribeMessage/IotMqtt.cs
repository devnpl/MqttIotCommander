using Daenet.Iot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using System.Text;
using System.Net;
using uPLibrary.Networking.M2Mqtt.Messages;



namespace SubscribeMessage
{
    public class IotMqtt : IIotApi
    {

        #region Private Section

        private MqttClient _mqttClient;
        private string _uniqueDeviceId;

        #endregion Private Section



        public string Name
        {
            get
            {
                return "myIotMqtt";
            }
        }

        public Task OnMessage(Func<object, bool> onReceiveMsg, CancellationToken cancelationToken, Dictionary<string, object> args = null)
        {
            throw new NotImplementedException();
        }

        public Task Open(Dictionary<string, object> args)
        {
            string connectionToMqtt = args.FirstOrDefault(x => x.Key.Contains("mqttConnectionString")).Value.ToString();

            return Task.Run(() =>
            {
                //_mqttClient = new MqttClient("broker.hivemq.com");
                _mqttClient = new MqttClient(connectionToMqtt);
                _uniqueDeviceId = Guid.NewGuid().ToString();
                _mqttClient.Connect(_uniqueDeviceId);


                //List<string> subTopicList = new List<string>();
                //List<byte> qosList = new List<byte>();
                //foreach (var item in args)
                //{
                //    if (item.Key.Contains("subscribeTopic"))
                //    {
                //        subTopicList.Add(item.Value.ToString());
                //        qosList.Add(MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE);
                //    }
                //}
                //_mqttClient.Subscribe(subTopicList.ToArray(), qosList.ToArray());

                //_mqttClient.MqttMsgPublishReceived += _mqttClient_MqttMsgPublishReceived;

                Console.WriteLine("Connected");
            });


        }

        private void _mqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {

            Dictionary<string, object> arg = new Dictionary<string, object>();
           //tring connectionToMqtt = arg.FirstOrDefault(x => x.Key.Contains("mqttConnectionString")).Value.ToString();
            arg.Add("sender", sender);
            arg.Add("e", e);

            ReceiveAsync(null, null, 60000, arg);
        }

        public void Open(string hostName, IIotApi client)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAsync(Func<object, bool> onSuccess = null, Func<Exception, bool> onError = null, int timeout = 60000, Dictionary<string, object> args =null)
        {
            //  throw new NotImplementedException();
            

            return Task.Run(() =>
            {
                List<string> subTopicList = new List<string>();
                List<byte> qosList = new List<byte>();
                foreach (var item in args)
                {
                    if (item.Key.Contains("subscribeTopic"))
                    {
                        subTopicList.Add(item.Value.ToString());
                        qosList.Add(MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE);
                    }
                }
                _mqttClient.Subscribe(subTopicList.ToArray(), qosList.ToArray());

                _mqttClient.MqttMsgPublishReceived += _mqttClient_MqttMsgPublishReceived;

                object sender = args.FirstOrDefault(x => x.Key.Equals("sender")).Value;
                MqttMsgPublishEventArgs e = (MqttMsgPublishEventArgs)(args.FirstOrDefault(x => x.Key.Equals("e")).Value);


                Console.WriteLine("Topic : " + e.Topic + "  Message :" + Encoding.ASCII.GetString(e.Message));
            });
        }
        public void RegisterAcknowledge(Action<string, Exception> onAcknowledgeReceived)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(IList<object> sensorMessages, Action<IList<object>> onSuccess = null, Action<IList<object>, Exception> onError = null, Dictionary<string, object> args = null)
        {

            throw new NotImplementedException();
            //List<string> pubMessages = new List<string>();
            //List<string> topicList = new List<string>();

            //foreach (object item in sensorMessages)
            //{
            //    pubMessages.Add(item.ToString());
            //}

            //foreach (var topic in args)
            //{
            //    if (topic.Key.Equals("topicName"))
            //    {
            //        topicList.Add(topic.Value.ToString());
            //    }
            //}

            //return Task.Run(() =>
            //{

            //    foreach (var ourTopic in topicList)
            //    {
            //        foreach (var msg in pubMessages)
            //        {
            //            _mqttClient.Publish(ourTopic, Encoding.ASCII.GetBytes(msg));
            //        }
            //    }

            //});
        }

        public Task SendAsync(object sensorMessage, Action<IList<object>> onSuccess = null, Action<IList<object>, Exception> onError = null, Dictionary<string, object> args = null)
        {
            throw new NotImplementedException();
        }
    }
}

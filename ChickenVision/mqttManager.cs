using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Mqtt;

namespace ChickenVision
{
    class mqttManager
    {
        public bool PostToQueue(string message)
        {

            using (var mqttClient = MqttClient.CreateAsync("10.0.0.50").Result)
            {
                // var mqttClient = MqttClient.CreateAsync("10.0.0.50").Result ;

                var sess = mqttClient.ConnectAsync().Result;

                string rcvTopic = "chickenai" + "/receive";
                string sendTopic = "chickenai" + "/command";

                mqttClient.SubscribeAsync(rcvTopic, MqttQualityOfService.ExactlyOnce);
                var sendData = String.Empty;

                Task.Run(() =>
                {
                    var line = message;
                    var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(line));
                    // var line = Regex.Unescape("{'command':" + command + "'}");
                    // var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(line.Replace("'","\"").Remove(0,1)));
                    sendData = data.ToString();
                    Console.WriteLine(System.DateTime.Now.ToString() + "------- MQTT: SENDTOPIC: " + sendTopic + " --------");
                    mqttClient.PublishAsync(new MqttApplicationMessage(sendTopic, data), MqttQualityOfService.ExactlyOnce).Wait();
                    
                });
                
            }

            return true;
        }
    }
}

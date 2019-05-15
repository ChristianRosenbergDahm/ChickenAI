using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
                var camName = "wyze_" + cameraID.ToString();

                string rcvTopic = "chickenai/" + camName + "/receive";
                string sendTopic = "chickenai/" + camName + "/command";

                mqttClient.SubscribeAsync(rcvTopic, MqttQualityOfService.ExactlyOnce);
                var sendData = String.Empty;

                Task.Run(() =>
                {
                    var line = command;
                    var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(line));
                    // var line = Regex.Unescape("{'command':" + command + "'}");
                    // var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(line.Replace("'","\"").Remove(0,1)));
                    sendData = data.ToString();
                    mqttClient.PublishAsync(new MqttApplicationMessage(sendTopic, data), MqttQualityOfService.ExactlyOnce).Wait();
                });
                var returnString = "------- MQTT: SENDTOPIC: " + sendTopic + " --------";
            }
        }
    }
}

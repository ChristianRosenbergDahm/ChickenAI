using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ChickenVision
{
    public class APIManager
    {
        public async Task<bool> IsDoorOpen()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Prediction-Key", "");
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                var values = new Dictionary<string, string>
               {
                    { "Url", "http://christiandahm.dk/edge-itn2/closed1.png" }
            };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://westeurope.api.cognitive.microsoft.com/customvision/v3.0/Prediction/4d013406-c917-4fc4-990f-e23914930051/classify/iterations/Iteration1/url", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var jsonObject = ParseStringToJSon(responseString);
                string openDoorStr = String.Empty;

                foreach (var item in jsonObject)
                {
                    if (item.Key == "predictions")
                    {
                        openDoorStr = item.Value.Last.First.Last.ToString();
                        //Console.WriteLine("Prediction open door: " + item.Value.Last.First.Last);
                    }
                }
                var mqttMan = new mqttManager();
                float openDoorfloat = float.Parse(openDoorStr);
                if (openDoorfloat > 50)
                {
                    Console.WriteLine(System.DateTime.Now.ToString() + " - Door might be open, prop: " + openDoorfloat);
                    // Notify MQTT queue
                    mqttMan.PostToQueue("ChickenDoorOpen");
                    return true;
                }
                else
                {
                    Console.WriteLine(System.DateTime.Now.ToString() + "  Door is Closed, prop: " + (1 - openDoorfloat));
                    mqttMan.PostToQueue("ChickenDoorClosed");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return true;
        }
        public JObject ParseStringToJSon(string data)
        {
            JObject json = JObject.Parse(data);
            return json;
        }

    }
}

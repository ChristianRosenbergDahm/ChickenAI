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
        public bool IsDoorOpen()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Prediction-Key", "");
                var values = new Dictionary<string, string>
               {
                    { "Url", "http://gusterd.asuscomm.com:92/Camera1/lastsnap.jpg" }
                    //{ "Url", "http://gusterd.asuscomm.com:92/picture/1/current/?_username=admin&_signature=185ed9f3de0740530dcef31ccf7bd346529388b3" }
                    

               };

                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync("https://westeurope.api.cognitive.microsoft.com/customvision/v3.0/Prediction/4d013406-c917-4fc4-990f-e23914930051/classify/iterations/Iteration1/url", content);
                var responseString = response.Result.Content.ReadAsStringAsync();
                var jsonObject = ParseStringToJSon(responseString.Result);
                string openDoorStr = String.Empty;

                foreach (var item in jsonObject)
                {
                    if (item.Key == "predictions")
                    {
                        openDoorStr = item.Value.First.First.Last.ToString();
                    }
                }
                var limit = float.Parse("0.5");
                float openDoorfloat = float.Parse(openDoorStr);
                if (openDoorfloat > limit)
                {
                    Console.WriteLine(System.DateTime.Now.ToString() + " - Door might be open, prop: " + openDoorfloat);
                    return true;
                }
                else
                {
                    Console.WriteLine(System.DateTime.Now.ToString() + "  Door is Closed, prop: " + (1 - openDoorfloat));
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                throw;
            }
        }
        public JObject ParseStringToJSon(string data)
        {
            JObject json = JObject.Parse(data);
            return json;
        }

        //public void SendMakerEventDoorIsOpen()
        //{
        //    var client = new HttpClient();
        //    //var response = client.GetAsync("https://maker.ifttt.com/trigger/ChickenDoorIsOpen/with/key/elUis2yEBQwp5OnALqburRiTGprWrwiSoiAV6uB-rRt");
        //    var response = client.GetAsync("https://maker.ifttt.com/trigger/ChickenAI/with/key/elUis2yEBQwp5OnALqburRiTGprWrwiSoiAV6uB-rRt");
        //    var responseString = response.Result.Content.ReadAsStringAsync();
        //    if (response.Result.IsSuccessStatusCode)
        //    {
        //        Console.WriteLine(System.DateTime.Now + " - IFTTT is notified: ");
        //    }
        //    else
        //    {
        //        Console.WriteLine(System.DateTime.Now + " - IFTTT notification went wrong: ");
        //    }
        //}

    }
}

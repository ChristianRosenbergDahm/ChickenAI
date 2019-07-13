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
                   
                    { "Url", "" }
               };

                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync("https://westeurope.api.cognitive.microsoft.com/customvision/v3.0/Prediction/4d013406-c917-4fc4-990f-e23914930051/classify/iterations/Iteration5/url", content);
                var responseString = response.Result.Content.ReadAsStringAsync();
                var jsonObject = ParseStringToJSon(responseString.Result);
                if (jsonObject.ToString().Contains("Invalid url:"))
                {
                    Console.WriteLine(System.DateTime.Now.ToString() + " - Cognitive service could not read image, invalid URL");
                }

                string openDoorStr = String.Empty;

                //Console.WriteLine(jsonObject.ToString());

                foreach (var item in jsonObject)
                {
                    if (item.Key == "predictions")
                    {
                        
                        var tagName = item.Value.First.Last.Last;
                        Console.WriteLine(System.DateTime.Now.ToString() + " - Tagname: " + tagName.ToString());
                        if (tagName.ToString().ToLower() == "Door is closed".ToLower())
                        {
                            openDoorStr = item.Value.First.First.Last.ToString();
                            float openDoorfloat = float.Parse(openDoorStr);
                            Console.WriteLine(System.DateTime.Now.ToString() + " - Door is Closed, prop: " + (openDoorfloat));
                            return false;
                        }

                        if (tagName.ToString().ToLower() == "Door is open".ToLower())
                        {
                            openDoorStr = item.Value.First.First.Last.ToString();
                            float openDoorfloat = float.Parse(openDoorStr);
                            Console.WriteLine(System.DateTime.Now.ToString() + " - Door is open, prop: " + (openDoorfloat));
                            return true;
                        }
                    }
                }

                Console.WriteLine(System.DateTime.Now.ToString() + " - Could not decide...");
                return true;

                //var limit = float.Parse("0.5");
                //float openDoorfloat = float.Parse(openDoorStr);
                //if (openDoorfloat > limit)
                //{
                //    Console.WriteLine(System.DateTime.Now.ToString() + " - Door might be open, prop: " + openDoorfloat);
                //    return true;
                //}
                //else
                //{
                //    Console.WriteLine(System.DateTime.Now.ToString() + "  Door is Closed, prop: " + (1 - openDoorfloat));
                //    return false;
                //}
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

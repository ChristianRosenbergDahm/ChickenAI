using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ChickenVision
{
    class HAApiManager
    {

        public void SetDoorOpenInHASS()
        {
            string longLivedToken = "";
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Add("Prediction-Key", "https://github.com/mfdahm/ChickenAI.git");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiI5MGVmNTBhYzdiM2Y0NGM2YTJlMWQzYTIwN2Y4MGQ3ZCIsImlhdCI6MTU1ODUyNzk4MSwiZXhwIjoxODczODg3OTgxfQ.1Nh9B6_zeRevFNDBSW8bKJPUrToeVtme7iCfZ2d0LJc");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            var values = new Dictionary<string, string>
               {
                    { "state", "on" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsync("http://localhost:8123/api/states/input_boolean.chicken_dooropen", content);
            var responseString = response.Result.Content.ReadAsStringAsync();
            Console.WriteLine(System.DateTime.Now.ToString() + " - Response from HA: " + responseString);
        }
    }
}

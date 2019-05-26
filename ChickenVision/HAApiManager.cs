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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            //client.DefaultRequestHeaders.Add("Content-Type","application/x-www-form-urlencoded");

            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            var values = new Dictionary<string, string>
               {
                    { "state","on" }
            };

            var stateOBJ = new ChickenState();
            stateOBJ.state = "on";
            var content = new FormUrlEncodedContent(values);
            var response = client.PostAsJsonAsync("http://10.0.0.50:8123/api/states/input_boolean.chicken_dooropen", stateOBJ);
            var responseString = response.Result.Content.ReadAsStringAsync();
            Console.WriteLine(System.DateTime.Now.ToString() + " - Response from HA: " + responseString.Status);
        }
    }

    class ChickenState
    {
        public string state { get; set; }
    }

}

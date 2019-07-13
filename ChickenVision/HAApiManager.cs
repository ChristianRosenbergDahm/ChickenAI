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
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            var stateOBJ = new ChickenState();
            stateOBJ.state = "on";
            var response = client.PostAsJsonAsync("http://10.0.0.50:8123/api/states/input_boolean.chicken_dooropen", stateOBJ);
            var responseString = response.Result.Content.ReadAsStringAsync();
            Console.WriteLine(System.DateTime.Now.ToString() + " - Response from HA: " + responseString.Status);
        }

        public void SetDoorClosedInHASS()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            var stateOBJ = new ChickenState();
            stateOBJ.state = "off";
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

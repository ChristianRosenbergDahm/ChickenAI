using System;

namespace ChickenVision
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(System.DateTime.Now.ToString() +  " - Chicken AI service started");

            var haMan = new HAApiManager();
            haMan.SetDoorOpenInHASS();

            var myManager = new APIManager();
            //var openDoor = myManager.IsDoorOpen();

            //Console.ReadLine();
        }
    }
}

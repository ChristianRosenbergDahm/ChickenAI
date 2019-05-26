using System;

namespace ChickenVision
{
    class Program
    {
        static void Main(string[] args)
        {
            //DateTime timeNine = Convert.ToDateTime("5 / 19 / 2019 15:00:00 PM");
            //DateTime timeNineFive = timeNine.AddMinutes(5);
            var sleep = 900000; // 15 minutes
            while (true)
            {
                //    if (System.DateTime.Now > timeNine & System.DateTime.Now < timeNineFive)
                //    {
                Console.WriteLine(System.DateTime.Now.ToString() + " - Chicken AI service started at nine");
                    var myManager = new APIManager();
                    var openDoor = myManager.IsDoorOpen();
                    if (openDoor == true)
                    {
                        var myHASSMan = new HAApiManager();
                        myHASSMan.SetDoorOpenInHASS();
                    }
                    Console.WriteLine(System.DateTime.Now.ToString() + " - Chicken AI service ended at nine");
                //}
                var nextCheck = System.DateTime.Now.AddMilliseconds(sleep);
                Console.WriteLine(System.DateTime.Now.ToString() + " - Sleeping, next check is: " + nextCheck.ToString());
                System.Threading.Thread.Sleep(sleep);
            }
        }
    }
}

using System;

namespace ChickenVision
{
    class Program
    {
        static void Main(string[] args)
        {
            //DateTime timeNine = Convert.ToDateTime("5 / 19 / 2019 15:00:00 PM");
            //DateTime timeNineFive = timeNine.AddMinutes(5);
            var sleep = 900000; // 15 minutes 900000
            var checkHours = 19; //19

            while (true)
            {
                // Only check af this time of day
                if (System.DateTime.Now.Hour >= checkHours)
                {

                    Console.WriteLine(System.DateTime.Now.ToString() + " - Chicken AI service started");
                    var myManager = new APIManager();
                    var openDoor = myManager.IsDoorOpen();
                    if (openDoor == true)
                    {
                        var myHASSMan = new HAApiManager();
                        myHASSMan.SetDoorOpenInHASS();
                    }
                    else
                    {
                        var myHASSMan = new HAApiManager();
                        myHASSMan.SetDoorClosedInHASS();
                    }

                    Console.WriteLine(System.DateTime.Now.ToString() + " - Chicken AI service ended");
                }
                else
                {
                    Console.WriteLine(System.DateTime.Now + " - Skipping check as time is before: " + checkHours);
                }

                var nextCheck = System.DateTime.Now.AddMilliseconds(sleep);
                Console.WriteLine(System.DateTime.Now.ToString() + " - Sleeping, next check is: " + nextCheck.ToString());
                System.Threading.Thread.Sleep(sleep);
            }
        }
    }
}

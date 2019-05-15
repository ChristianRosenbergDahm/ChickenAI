using System;

namespace ChickenVision
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var myManager = new APIManager();
            var openDoor = myManager.IsDoorOpen();

            Console.ReadLine();
        }
    }
}

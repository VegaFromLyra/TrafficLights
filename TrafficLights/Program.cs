using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press the Enter key to exit the program.");

            TrafficLight light1 = new TrafficLight();
            TrafficLight light2 = new TrafficLight();

            List<TrafficLight> trafficLights = new List<TrafficLight>();

            trafficLights.Add(light1);
            trafficLights.Add(light2);

            // in seconds
            int redLightTime = 5;
            int greenLightTime = 5;
            int orangeLightTime = 2;

            TrafficLightController instance = TrafficLightController.GetInstance(trafficLights, redLightTime, greenLightTime, orangeLightTime);

            instance.TrafficLightStateUpdate += light1.UpdateTrafficLight;
            instance.TrafficLightStateUpdate += light2.UpdateTrafficLight;

            Console.ReadLine();
        }
    }
}

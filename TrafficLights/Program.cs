using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Model traffic lights for an intersection. Write a function to operate it. 
namespace TrafficLights
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press the Enter key to exit the program.");

            TrafficLight light1 = new TrafficLight(1);
            TrafficLight light2 = new TrafficLight(2);

            List<TrafficLight> trafficLights = new List<TrafficLight>();

            trafficLights.Add(light1);
            trafficLights.Add(light2);

            // in seconds
            // Typicall RedTimeUnits = GreenTimeUnits + OrangeTimeUnits
            int redLightTime = 5;
            int greenLightTime = 3;
            int orangeLightTime = 2;

            TrafficLightController instance = TrafficLightController.GetInstance(trafficLights, redLightTime, greenLightTime, orangeLightTime);

            instance.TrafficLightStateUpdate += light1.UpdateTrafficLight;
            instance.TrafficLightStateUpdate += light2.UpdateTrafficLight;

            Console.ReadLine();
        }
    }
}

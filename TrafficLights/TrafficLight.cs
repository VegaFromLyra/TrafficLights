using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    public enum TRAFFIC_LIGHT_COLOR
    {
        RED,
        GREEN,
        ORANGE
    };


    public class TrafficLight
    {
        public TRAFFIC_LIGHT_COLOR State { get; set; }

        private int ID;

        private void PrintState(TRAFFIC_LIGHT_COLOR state)
        {
            if (!TrafficLights.ContainsKey(state))
            {
                Console.WriteLine("Invalid traffic color");
            }

            Console.WriteLine(TrafficLights[state]);
        }

        private Dictionary<TRAFFIC_LIGHT_COLOR, string> TrafficLights = new Dictionary<TRAFFIC_LIGHT_COLOR, string>();

        public TrafficLight(int id)
        {
            ID = id;
            TrafficLights.Add(TRAFFIC_LIGHT_COLOR.RED, "Red");
            TrafficLights.Add(TRAFFIC_LIGHT_COLOR.GREEN, "Green");
            TrafficLights.Add(TRAFFIC_LIGHT_COLOR.ORANGE, "Orange");
        }

        public void UpdateTrafficLight(object Sender, EventArgs e)
        {
            State =  (TRAFFIC_LIGHT_COLOR)(((int)(State + 1)) % TrafficLights.Count);
            PrintState(State);
        }

    }
}

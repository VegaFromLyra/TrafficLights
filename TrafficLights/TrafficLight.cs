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
        private TRAFFIC_LIGHT_COLOR state;
        public TRAFFIC_LIGHT_COLOR State 
        {
            get
            {
                return state;
            }

            set
            {  
                state = value;
                PrintState(state);
            }
        }

        private void PrintState(TRAFFIC_LIGHT_COLOR state)
        {
            if (!TrafficLights.ContainsKey(state))
            {
                throw new Exception("Invalid traffic color");
            }

            Console.WriteLine(TrafficLights[state]);
        }


        private Dictionary<TRAFFIC_LIGHT_COLOR, string> TrafficLights = new Dictionary<TRAFFIC_LIGHT_COLOR, string>();

        public TrafficLight()
        {
            TrafficLights.Add(TRAFFIC_LIGHT_COLOR.RED, "Red");
            TrafficLights.Add(TRAFFIC_LIGHT_COLOR.GREEN, "Green");
            TrafficLights.Add(TRAFFIC_LIGHT_COLOR.ORANGE, "Orange");
        }

        public void UpdateTrafficLight(object Sender, EventArgs e)
        {
            State = (TRAFFIC_LIGHT_COLOR)((int)(state + 1) % TrafficLights.Count);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TrafficLights
{
    // Traffic light controller for 2 way traffic signal
    public class TrafficLightController
    {
        private static TrafficLightController instance;
        private Timer TrafficLightTimer;
        private static DateTime lastRecordedTime;

        private enum TRAFFIC_CONTROLLER_STATE
        {
            Red,
            Green,
            Yellow
        };

        private TrafficLightController(List<TrafficLight> trafficLights,
                                       int redTimeInterval,
                                       int greenTimeInterval,
                                       int yellowTimeInterval)
        {
            TrafficLights = trafficLights;

            if (redTimeInterval == 0 || greenTimeInterval == 0 || yellowTimeInterval == 0)
            {
                throw new Exception("All time intervals should be greater than 0");
            }

            RTimeUnit = redTimeInterval;
            GTimeUnit = greenTimeInterval;
            YTimeUnit = yellowTimeInterval;

            if (trafficLights.Count % 2 != 0)
            {
                throw new Exception("Number of traffic lights should be even");
            }

            for (int i = 0; i < trafficLights.Count; i++)
            {
                trafficLights[i].State = TRAFFIC_LIGHT_COLOR.RED;
                // Print initial state
                Console.WriteLine("Red");
            }

            lastRecordedTime = DateTime.Now;
            currentState = TRAFFIC_CONTROLLER_STATE.Red;

            // Create a timer with a 1 second interval.
            TrafficLightTimer = new Timer(1000);

            // Hook up the Elapsed event for the timer.
            TrafficLightTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            TrafficLightTimer.Enabled = true;
        }


        public static TrafficLightController GetInstance(List<TrafficLight> trafficLights,
                                                         int redTimeInterval,
                                                         int greenTimeInterval,
                                                         int yellowTimeInterval)
        {           
            if (instance == null)
            {
                instance = new TrafficLightController(trafficLights, redTimeInterval, greenTimeInterval, yellowTimeInterval);
            }

            return instance;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (
                 ((int)(e.SignalTime - lastRecordedTime).TotalSeconds == RTimeUnit) && 
                 (currentState == TRAFFIC_CONTROLLER_STATE.Red)
               )
            {
                UpdateState(TRAFFIC_CONTROLLER_STATE.Green);
            }
            else if 
                (
                 ((int)(e.SignalTime - lastRecordedTime).TotalSeconds == GTimeUnit) &&
                 (currentState == TRAFFIC_CONTROLLER_STATE.Green)
                )
            {
                UpdateState(TRAFFIC_CONTROLLER_STATE.Yellow);
            }
            else if
                (
                  ((int)(e.SignalTime - lastRecordedTime).TotalSeconds == YTimeUnit) && 
                  (currentState == TRAFFIC_CONTROLLER_STATE.Yellow)
                )
            {
                UpdateState(TRAFFIC_CONTROLLER_STATE.Red);
            }
            else
            {
                Console.Write(".");
            }
        }

        private void UpdateState(TRAFFIC_CONTROLLER_STATE newState)
        {
            Console.WriteLine();
            lastRecordedTime = DateTime.Now;
            currentState = newState;
            OnTrafficLightStateUpdate(EventArgs.Empty);
        }

        protected virtual void OnTrafficLightStateUpdate(EventArgs e)
        {
            if (instance == null)
            {
                throw new Exception("BUG: Instance cannot be null when traffic controller state is changing");
            }

            EventHandler handler = TrafficLightStateUpdate;

            if (handler != null)
            {
                handler(instance, e);
            }

        }

        public event EventHandler TrafficLightStateUpdate;

        public List<TrafficLight> TrafficLights
        {
            get;
            private set;
        }

        private int YTimeUnit;
        private int GTimeUnit;
        private int RTimeUnit;

        private TRAFFIC_CONTROLLER_STATE currentState;
    }

}

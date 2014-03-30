using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TrafficLights
{
    public class TrafficLightController
    {
        private static TrafficLightController instance;
        private System.Timers.Timer TrafficLightTimer;
        private static DateTime lastRecordedTime;

        private enum TRAFFIC_CONTROLLER_STATE
        {
            Initial,
            MajorTimeInterval1Elapsed,
            MajorTimeInterval2Elapsed,
            MinorTimeIntervalElapsed
        };

        private TrafficLightController(List<TrafficLight> trafficLights, int majorTimeInterval1, int majorTimeInterval2, int minorTimeInterval)
        {
            TrafficLights = trafficLights;

            if (majorTimeInterval1 == 0 || majorTimeInterval2 == 0 || minorTimeInterval == 0)
            {
                throw new Exception("All time intervals should be greater than 0");
            }

            this.majorTimeInterval1 = majorTimeInterval1;
            this.majorTimeInterval2 = majorTimeInterval2;
            this.minorTimeInterval = minorTimeInterval;

            if (trafficLights.Count % 2 != 0)
            {
                throw new Exception("Number of traffic lights should be even");
            }

            for (int i = 0; i < trafficLights.Count; i++)
            {
                trafficLights[i].State = TRAFFIC_LIGHT_COLOR.RED;
            }

            lastRecordedTime = DateTime.Now;
            currentState = TRAFFIC_CONTROLLER_STATE.Initial;

            // Create a timer with a 1 second interval.
            TrafficLightTimer = new System.Timers.Timer(1000);

            // Hook up the Elapsed event for the timer.
            TrafficLightTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            TrafficLightTimer.Enabled = true;
        }


        public static TrafficLightController GetInstance(List<TrafficLight> trafficLights, int majorTimeInterval1, int majorTimeInterval2, int minorTimeInterval)
        {           
            if (instance == null)
            {
                instance = new TrafficLightController(trafficLights, majorTimeInterval1, majorTimeInterval2, minorTimeInterval);
            }

            return instance;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (
                 ((int)(e.SignalTime - lastRecordedTime).TotalSeconds == majorTimeInterval1) && 
                 (currentState == TRAFFIC_CONTROLLER_STATE.Initial || currentState == TRAFFIC_CONTROLLER_STATE.MinorTimeIntervalElapsed)
               )
            {
                UpdateState(TRAFFIC_CONTROLLER_STATE.MajorTimeInterval1Elapsed);
            }
            else if 
                (
                 ((int)(e.SignalTime - lastRecordedTime).TotalSeconds == majorTimeInterval2) &&
                 (currentState == TRAFFIC_CONTROLLER_STATE.MajorTimeInterval1Elapsed)                  
                )
            {
                UpdateState(TRAFFIC_CONTROLLER_STATE.MajorTimeInterval2Elapsed);
            }
            else if
                (
                  ((int)(e.SignalTime - lastRecordedTime).TotalSeconds == minorTimeInterval) && (currentState == TRAFFIC_CONTROLLER_STATE.MajorTimeInterval2Elapsed)
                )
            {
                UpdateState(TRAFFIC_CONTROLLER_STATE.MinorTimeIntervalElapsed);
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

        private int majorTimeInterval1;
        private int majorTimeInterval2;
        private int minorTimeInterval;

        private TRAFFIC_CONTROLLER_STATE currentState;
    }

}

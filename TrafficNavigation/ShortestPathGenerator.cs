using System.Collections.Generic;
using System.Linq;

namespace TrafficNavigation
{
    public class ShortestPathGenerator
    {
        /// <summary>
        /// Generates shortest path available with the current vehicle among given orbits 
        /// </summary>
        /// <param name="orbits"></param>
        /// <param name="climate"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static KeyValuePair<Orbit,double> GenerateShortestPathForVehicle(Orbit[] orbits, Climate climate,Vehicles v)
        {
            List<KeyValuePair<Orbit, double>> alllists = new List<KeyValuePair<Orbit, double>>();
            List<KeyValuePair<Orbit, double>> incompleteDestination = new List<KeyValuePair<Orbit, double>>();
            for (int i = 0; i < orbits.Length; i++)
            {
                var path = VehicleTimeGenerator.GetVehicleTime(orbits[i], climate,v);
                if (orbits[i].IsBreakJourney)
                    incompleteDestination.Add(new KeyValuePair<Orbit, double>(orbits[i], path.Value));
                else
                alllists.Add(new KeyValuePair<Orbit, double>(orbits[i],path.Value));
            }
            if (incompleteDestination.Any())
            {
                alllists.Add(new KeyValuePair<Orbit, double>(incompleteDestination.Last().Key, incompleteDestination.Sum(i => i.Value)));
            }
            var minTime = alllists.Min(a => a.Value);
            var shortestTime = alllists.First(a => a.Value == minTime);
            return shortestTime;
        }
    }
}

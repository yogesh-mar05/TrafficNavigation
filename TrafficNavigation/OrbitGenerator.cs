using System;
using System.Collections.Generic;
using System.Linq;

namespace TrafficNavigation
{
    public class OrbitGenerator
    {
        /// <summary>
        /// Generates correct orbit for both similar as well as non similar destination
        /// </summary>
        /// <param name="orbits"></param>
        /// <param name="climate"></param>
        /// <param name="isSameDestination"></param>
        /// <returns></returns>
        public static List<CorrectOrbit> GenerateCorrectRoute(Orbit[] orbits, Climate climate,bool isSameDestination)
        {
            if (!isSameDestination)
            {
                double overallCarTime = Double.MaxValue;
                KeyValuePair<Orbit, double> carShortestPathToD2;
                orbits[2].IsBreakJourney = true;
                orbits[3].IsBreakJourney = true;
                GetShortestPathToD1(orbits, climate, out KeyValuePair<Orbit, double> carShortestPathToD1,
                    out KeyValuePair<Orbit, double> bikeShortestPathToD1,
                    out KeyValuePair<Orbit, double> ttShortestPathToD1);
                orbits[2].IsBreakJourney = false;
                if (carShortestPathToD1.Key.IsBreakJourney)
                {
                    carShortestPathToD2 = ShortestPathGenerator.GenerateShortestPathForVehicle(new Orbit[] { orbits[2] }, climate, new Car());
                    overallCarTime = carShortestPathToD1.Value;
                }
                else
                {
                    carShortestPathToD1.Key.IsBreakJourney = true;
                    carShortestPathToD2 = ShortestPathGenerator.GenerateShortestPathForVehicle(new Orbit[] { carShortestPathToD1.Key, orbits[3] }, climate, new Car());
                    carShortestPathToD1.Key.IsBreakJourney = false;
                    overallCarTime = carShortestPathToD2.Value;
                }
                double overallBikeTime = Double.MaxValue;
                KeyValuePair<Orbit, double> bikeShortestPathToD2;
                if (bikeShortestPathToD1.Key.IsBreakJourney)
                {
                    bikeShortestPathToD2 = ShortestPathGenerator.GenerateShortestPathForVehicle(new Orbit[] { orbits[2] }, climate, new Bike());
                    overallBikeTime = bikeShortestPathToD1.Value;
                }
                else
                {
                    bikeShortestPathToD1.Key.IsBreakJourney = true;
                    bikeShortestPathToD2 = ShortestPathGenerator.GenerateShortestPathForVehicle(new Orbit[] { bikeShortestPathToD1.Key, orbits[3] }, climate, new Bike());
                    bikeShortestPathToD1.Key.IsBreakJourney = false;
                    overallBikeTime = bikeShortestPathToD2.Value;
                }
                KeyValuePair<Orbit, double> ttShortestPathToD2;
                double ttOverAllTime = Double.MaxValue;
                if (ttShortestPathToD1.Key.IsBreakJourney)
                {
                    ttShortestPathToD2 = ShortestPathGenerator.GenerateShortestPathForVehicle(new Orbit[] { orbits[2] }, climate, new TukTuk());
                    ttOverAllTime = ttShortestPathToD1.Value;
                }
                else
                {
                    ttShortestPathToD1.Key.IsBreakJourney = true;
                    ttShortestPathToD2 = ShortestPathGenerator.GenerateShortestPathForVehicle(new Orbit[] { ttShortestPathToD1.Key, orbits[3] }, climate, new TukTuk());
                    ttOverAllTime = ttShortestPathToD2.Value;
                }
                var shortestPath = new List<CorrectOrbit>();
                shortestPath = overallCarTime < overallBikeTime
                    && overallCarTime < ttOverAllTime ? GenerateCorrectOrbit(new
                    List<KeyValuePair<Orbit, double>>() { carShortestPathToD1, carShortestPathToD2 },
                    Vehicle.Car) :
                    ttOverAllTime < overallBikeTime ?
                    GenerateCorrectOrbit(new
                    List<KeyValuePair<Orbit, double>>() { ttShortestPathToD1, ttShortestPathToD2 },
                    Vehicle.TukTuk) :
                    GenerateCorrectOrbit(new
                    List<KeyValuePair<Orbit, double>>() { bikeShortestPathToD1 , bikeShortestPathToD2 },
                    Vehicle.Bike);

                return shortestPath;
            }
            else
                return GetSameDestinationCorrectOrbit(orbits, climate);
        }

        private static List<CorrectOrbit> GetSameDestinationCorrectOrbit(Orbit[] orbits, Climate climate)
        {

            GetShortestPathToD1(orbits, climate, out KeyValuePair<Orbit, double> carShortestPathToD1,
                out KeyValuePair<Orbit, double> bikeShortestPathToD1,
                out KeyValuePair<Orbit, double> ttShortestPathToD1);
            var shortestPath = carShortestPathToD1.Value < bikeShortestPathToD1.Value
                    && carShortestPathToD1.Value < ttShortestPathToD1.Value ? GenerateCorrectOrbit(new
                    List<KeyValuePair<Orbit, double>>() { carShortestPathToD1 },
                    Vehicle.Car) :
                    ttShortestPathToD1.Value < bikeShortestPathToD1.Value ?
                    GenerateCorrectOrbit(new
                    List<KeyValuePair<Orbit, double>>() { ttShortestPathToD1 },
                    Vehicle.TukTuk) :
                    GenerateCorrectOrbit(new
                    List<KeyValuePair<Orbit, double>>() { bikeShortestPathToD1 },
                    Vehicle.Bike);
            return shortestPath;
        }

        private static void GetShortestPathToD1(Orbit[] orbits, Climate climate, out KeyValuePair<Orbit, double> carShortestPathToD1, out KeyValuePair<Orbit, double> bikeShortestPathToD1, out KeyValuePair<Orbit, double> ttShortestPathToD1)
        {
            carShortestPathToD1 = ShortestPathGenerator.GenerateShortestPathForVehicle(orbits, climate, new Car());
            bikeShortestPathToD1 = ShortestPathGenerator.GenerateShortestPathForVehicle(orbits, climate, new Bike());
            ttShortestPathToD1 = ShortestPathGenerator.GenerateShortestPathForVehicle(orbits, climate, new TukTuk());
        }

        private static List<CorrectOrbit> GenerateCorrectOrbit(List<KeyValuePair<Orbit, double>> currentOrbit, Vehicle car)
        {
            return 
                currentOrbit.Select(c=>
                new CorrectOrbit()
                {
                    TypeofVehicle = car,
                    Destination = c.Key.Destination,
                    Orbit = c.Key.OrbitName,
                    TimeTaken = c.Value
                }).ToList();            
        }        
    }
}

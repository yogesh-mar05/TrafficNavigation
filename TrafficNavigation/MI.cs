using System;
using System.Collections.Generic;
using System.Linq;

namespace TrafficNavigation
{
    public class MI
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose the problem: \n" +
                "1.Problem1 \n" +
                "2.Problem2: Mission Impossible");
            int problem = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter the weather type:" + "\n" +
                "1.Sunny" + "\n" +
                "2.Windy" + "\n" +
                "3.Rainy" + "\n");

            Climate c = (Climate)Convert.ToInt32(Console.ReadLine());
            Random r = new Random();

            Orbit orbit1 = new Orbit()
            {
                Craters = 20,
                Distance = 18,
                TrafficSpeed = GetTrafficSpeed(r),
                OrbitName = "Orbit1",
                Destination = "Hallithiran"
            };
            Orbit orbit2 = new Orbit()
            {
                Craters = 10,
                Distance = 20,
                TrafficSpeed = GetTrafficSpeed(r),
                OrbitName = "Orbit2",
                Destination = "Hallithiran"
            };
            Orbit orbit3 = new Orbit()
            {
                Craters = 15,
                Distance = 30,
                TrafficSpeed = GetTrafficSpeed(r),
                OrbitName = "Orbit3",
                Destination = "RKPuram"
            };
            Orbit orbit4 = new Orbit()
            {
                Craters = 18,
                Distance = 15,
                TrafficSpeed = GetTrafficSpeed(r),
                OrbitName = "Orbit4",
                Destination = "Hallithiran"
            };
            Orbit[] orbitProblem1 = new Orbit[] { orbit1, orbit2 };
            Orbit[] orbitProblem2 = new Orbit[] { orbit1, orbit2, orbit3, orbit4 };
            List<CorrectOrbit> orbits = new List<CorrectOrbit>();
            if (problem == 1)
            {
                orbits = OrbitGenerator.GenerateCorrectRoute(orbitProblem1, c, true);
            }
            else
            {
                orbits = OrbitGenerator.GenerateCorrectRoute(orbitProblem2, c, false);

                orbits.First().Destination = "Hallithiran";
                orbits[1].Destination = "RKPuram";
            }
            
            foreach (var item in orbits)
            {
                Console.WriteLine("The Shortest time taken to " + item.Destination + " is " + (item.TimeTaken * 60) +
                " minutes via " + item.Orbit.ToString() + " using " + item.TypeofVehicle.ToString()
                + " with the current traffic being " + orbitProblem2.First(o => o.OrbitName == item.Orbit).TrafficSpeed);
            }
            Console.Read();
        }

        private static int GetTrafficSpeed(Random r)
        {
            return r.Next(5, 25);
        }

    }
}

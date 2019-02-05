using System;
using System.Collections.Generic;

namespace TrafficNavigation
{
    public static class VehicleTimeGenerator
    {
        public static readonly double[] climateCraters = new double[] { Constants.sunnyCrater, Constants.windyCrater, Constants.rainyCrater };
        /// <summary>
        /// Generates the time taken for the vehicle given in the current orbit
        /// </summary>
        /// <param name="orbit"></param>
        /// <param name="climate"></param>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
        public static KeyValuePair<Vehicle, double> GetVehicleTime(Orbit orbit, Climate climate,Vehicles vehicleType)
        {
            if (vehicleType.AverseToWeather == climate)
                return new KeyValuePair<Vehicle, double>(vehicleType.TypeOfVehicle, Double.MaxValue);
            var vehicleSpeed = vehicleType.Speed<orbit.TrafficSpeed?vehicleType.Speed:orbit.TrafficSpeed;
            double cTime = orbit.Distance/vehicleSpeed + (orbit.Craters * 
                vehicleType.CraterSpeed * climateCraters[(int)climate - 1]);
            return new KeyValuePair<Vehicle, double>(vehicleType.TypeOfVehicle, cTime);
        }
    }
}

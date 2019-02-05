using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficNavigation
{
    public class Car:Vehicles
    {
        public override double Speed => Constants.cspeed;
        public override double CraterSpeed => Constants.cCrater;
        public override Vehicle TypeOfVehicle => Vehicle.Car;
        public override Climate AverseToWeather => Climate.None;
    }
}

namespace TrafficNavigation
{
    public class Bike:Vehicles
    {
        public override double Speed => Constants.bSpeed;
        public override double CraterSpeed => Constants.bCrater;
        public override Vehicle TypeOfVehicle => Vehicle.Bike;
        public override Climate AverseToWeather => Climate.Rainy;
    }
}

namespace TrafficNavigation
{
    public class TukTuk:Vehicles
    {
        public override double Speed => Constants.ttSpeed;
        public override double CraterSpeed => Constants.ttCrater;
        public override Vehicle TypeOfVehicle => Vehicle.TukTuk;
        public override Climate AverseToWeather => Climate.Windy;
    }
}

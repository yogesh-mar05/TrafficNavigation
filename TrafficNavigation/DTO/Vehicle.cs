namespace TrafficNavigation
{
    public class Vehicles
    {
        public virtual double Speed { get; set; }
        public virtual double CraterSpeed { get; set; }
        public virtual Vehicle TypeOfVehicle { get; set; }
        public virtual Climate AverseToWeather { get; set; }
    }
}

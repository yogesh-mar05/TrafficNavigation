namespace TrafficNavigation
{
    public class Orbit
    {
        public int Craters { get; set; }
        public int Distance { get; set; }
        public double TrafficSpeed { get; internal set; }
        public string Destination { get; set; }
        public string OrbitName { get; set; }
        public bool IsBreakJourney { get; set; }
    }
}

namespace RouteActor.Interfaces.Entities
{
    public class Waypoint
    {
        public string CompassDirection { get; set; }
        public decimal ManeuverPointLong { get; set; }
        public decimal ManeuverPointLat { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;

namespace RouteActor.Interfaces.Entities
{
    public class RouteEntity
    {
        public decimal StartLongitude { get; set; }
        public decimal StartLatitude { get; set; }
        public decimal EndLongitue { get; set; }
        public decimal EndLatitude { get; set; }
        public Guid Id { get; set; }
        public List<Waypoint> Waypoints { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway
{
    public enum Direction
    {
        NORTH = 10,
        NORTHEAST = 15,
        EAST = 20,
        SOUTHEAST = 25,
        SOUTH = 30,
        SOUTHWEST = 35,
        WEST = 40,
        NORTHWEST = 45

    }
    public class NavigationCommand
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public Direction Direction { get; set; }
    }
}

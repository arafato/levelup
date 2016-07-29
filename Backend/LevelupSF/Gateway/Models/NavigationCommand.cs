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
        EAST = 20,
        SOUTH = 30,
        WEST = 40,
    }
    public class NavigationCommand
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public Direction Direction { get; set; }
    }
}

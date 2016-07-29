using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway
{
    public enum Direction
    {
        STRAIGHT = 10,
        LEFT = 20,
        Right = 30
    }
    public class NavigationCommand
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public Direction Direction { get; set; }
    }
}

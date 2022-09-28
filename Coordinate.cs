using System;
using System.Collections.Generic;
using System.Text;

namespace LavaTubes
{
 
    //The class represents a coordinate on the map representation. It has an x, y value, and a height
    public class Coordinate
    {

        public int x { get; set; }
        public int y { get; set; }
        public int height { get; set; }
        
        public Coordinate(int x, int y, int height) 
        {
            this.x = x;
            this.y = y;
            this.height = height;
        }
    }
}

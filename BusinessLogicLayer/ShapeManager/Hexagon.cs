using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BusinessLogicLayer.ShapeManager
{
    public class Hexagon : ShapeBase
    {

        public Hexagon(double width, double height) : base(width, height)
        {
        }
        public Hexagon() : this(25, 25)
        {
            this.Name = "Hexagon";
        }
        protected override Geometry DefiningGeometry
        {
            get
            {
                return Geometry.Parse("M 5,0 L0,-10 L5,-20 L10,-20 L15,-10 L10,0 Z");
            }
        }
    }
}

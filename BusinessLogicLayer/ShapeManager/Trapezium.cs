using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BusinessLogicLayer.ShapeManager
{
    public class Trapezium : ShapeBase
    {
        public Trapezium(double width, double height) : base(width, height)
        {
        }
        public Trapezium():this(25,25)
        {
            Name = "Trapezium";
        }

        protected override Geometry DefiningGeometry => Geometry.Parse("M 0,0 V-15 H25 L40,0 L0,0 M 25,0 V-15");
    }
}

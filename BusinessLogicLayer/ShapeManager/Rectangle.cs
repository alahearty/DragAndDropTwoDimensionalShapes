using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BusinessLogicLayer.ShapeManager
{
    public class Rectangle : ShapeBase
    {
        public Rectangle(double width, double height) : base(width, height)
        {
        }
        public Rectangle():this(25,25)
        {
            Name = "Rectangle";
        }
        protected override Geometry DefiningGeometry => Geometry.Parse("M 5,5 v10 H20 L25,10 L20,5 Z");
    }
}

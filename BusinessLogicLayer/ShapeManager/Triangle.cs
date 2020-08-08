using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BusinessLogicLayer.ShapeManager
{
    public class Triangle : ShapeBase
    {
        public Triangle(double width, double height) : base(width, height)
        {
        }
        public Triangle() : this(25, 25)
        {
           this.Name = "Triangle";
        }
        protected override Geometry DefiningGeometry => Geometry.Parse("M 15,5 L5,15 H25 Z");

    }
}

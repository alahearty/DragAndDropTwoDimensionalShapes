using System.Windows.Media;
using System.Windows.Shapes;

namespace BusinessLogicLayer.ShapeManager
{
    public class Square : ShapeBase
    {
       
        public Square(double width, double height) : base(width, height)
        {           
        }

        public Square() : this(25, 25)
        {
            this.Name = "Square";
        }
        protected override Geometry DefiningGeometry => Geometry.Parse("M 0,-50 H100 V-100 H0 Z");
    }
}

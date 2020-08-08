using System.Windows.Media;

namespace BusinessLogicLayer.ShapeManager
{
    public class Circle : ShapeBase
    {      
        public Circle(double width, double height) : base(width, height)
        {
        }

        public Circle() : this(25, 25)
        {
            this.Name = "Circle";
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                return Geometry.Parse("M 0,0 A 180, 180 180 1 1 1, 1 Z");
            }
        }
    }
}
using System.Windows;
using System.Windows.Media;

namespace BusinessLogicLayer.ShapeManager
{
    public class Star : ShapeBase
    {
        //protected override Geometry DefiningGeometry => Geometry.Parse("M 0,0 V-10 H15 L20,-5 L15,0 Z");
        //protected override Geometry DefiningGeometry => Geometry.Parse("M 15,5 L5,15 H25 L15,5");
        protected override Geometry DefiningGeometry => Geometry.Parse("M 15,0 L10,10 L5,15 L10,20 L15,30 L20,20 L25,15 L20, 10 Z");

        public Star(double width, double height) : base(width, height)
        {
        }
        public Star() : this(25, 25)
        {
            Name = "Star";
        }
        
    }
}

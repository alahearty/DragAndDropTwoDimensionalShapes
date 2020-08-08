using BusinessLogicLayer.ShapeManager;
using System.Windows.Media;

namespace BusinessLogicLayer.ShapeManager
{
    public class Cross : ShapeBase
    {
        public Cross(double width, double height) : base(width, height)
        {
           
        }
        public Cross() : this(25, 25)
        {
            this.Name = "Cross";
        }
        protected override Geometry DefiningGeometry => Geometry.Parse("M 50,0 H60 V-20 H75 V-30 H60 V-50 H50 V-30 H35 V-20 H50 V0 Z");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BusinessLogicLayer.ShapeManager
{
    public class Quadrilateral : ShapeBase
    {
        public Quadrilateral(double width, double height) : base(width, height)
        {
        }
        public Quadrilateral(): this(25, 25)
        {
            this.Name = "Quadrilateral";
        }

        protected override Geometry DefiningGeometry => Geometry.Parse("M 5,0 L0,-10 L5,-20  L10,-10  Z");
    }
    
}

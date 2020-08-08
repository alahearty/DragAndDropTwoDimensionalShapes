using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BusinessLogicLayer.ShapeManager
{
    public class Line : ShapeBase
    {
        public Line(double width, double height) : base(width, height)
        {
        }
        public Line() : this(25, 25)
        {
            this.Name = "Line";
        }
        protected override Geometry DefiningGeometry
        {
            get
            {
                return Geometry.Parse("M 10,50 L 200,70");
                // return Geometry.Parse("M 0,-50 H50 V-50 H0 Z");
            }
        }
    }
}

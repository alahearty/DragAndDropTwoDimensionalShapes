using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BusinessLogicLayer.ShapeManager
{
    public class CurveArc : ShapeBase
    {
        public CurveArc(double width, double height) : base(width, height)
        {
        }
        public CurveArc():this(25,25)
        {
            Name = "CurveArc";
        }
        protected override Geometry DefiningGeometry =>  Geometry.Parse("M 10,100 C 10,300 300,-200 300,100");

    }
}

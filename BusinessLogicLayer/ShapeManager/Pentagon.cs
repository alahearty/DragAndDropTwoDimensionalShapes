using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BusinessLogicLayer.ShapeManager
{
    public class Pentagon : ShapeBase
    {
        
        public Pentagon(double width, double height) : base(width, height)
        {
        }
        public Pentagon() : this(25, 25)
        {
            this.Name = "Pentagon";
        }
        protected override Geometry DefiningGeometry
        {
            get
            {
                return Geometry.Parse("M 2,0 L0,-20 L10,-30 L20,-20 L18,0 Z");

            }
        }
    }
}

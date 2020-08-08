using BusinessLogicLayer.ShapeManager;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace TwoDimensionShapeApp.ToolBox
{
    public class ToolBoxViewModel : BindableBase
    {
        public List<Shape> Shapes { get; }
        public ToolBoxViewModel()
        {
            Shapes = _shapes();
        }
        private List<Shape> _shapes()
        {
            var shapes = new List<Shape>();
            var triagle = new Triangle();
            var square = new Square();
            var cross = new Cross();
            var circle = new Circle();
            var quadrilateral = new Quadrilateral();
            var pentagon = new Pentagon();
            var hexagon = new Hexagon();
            var line = new BusinessLogicLayer.ShapeManager.Line();
            var star = new Star();
            var trapezium = new Trapezium();
            var curvedArc = new CurveArc();
            var rectangle = new BusinessLogicLayer.ShapeManager.Rectangle();

            shapes.Add(circle);
            shapes.Add(pentagon);
            shapes.Add(triagle);
            shapes.Add(line);
            shapes.Add(square);
            shapes.Add(hexagon);
            shapes.Add(quadrilateral);
            shapes.Add(cross);
            shapes.Add(star);
            shapes.Add(trapezium);
            shapes.Add(curvedArc);
            shapes.Add(rectangle);

            return shapes;
        }
    }
}

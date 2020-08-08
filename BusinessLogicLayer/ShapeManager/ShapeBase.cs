using DataAccessLayer;
using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BusinessLogicLayer.ShapeManager
{
    public abstract class ShapeBase : Shape, IEntityBase
    {
        private readonly Brush itemStrokeInToolBox = Brushes.White;
        private Brush currentStroke = Brushes.Black;

        public ShapeBase(double width, double height)
        {
            Width = width;
            Height = height;
            Stretch = Stretch.Fill;
            Fill = Brushes.Transparent;
            Stroke = itemStrokeInToolBox;
            Margin = new System.Windows.Thickness(1);            
        }

        private int identity;
        public int Identity
        {
            get => identity;
            set
            {
                if (value <= 0) return;
                identity = value;
            }
        }

        public Guid Id { get; set; } = Guid.Empty;
        public bool IsSelected { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }

        public void SetCurrentStroke(Brush brush)
        {
            currentStroke = brush;
            Stroke = currentStroke;
        }
        public void SetToOriginalStroke()
        {
            Stroke = currentStroke;
        }
        public void AddSelection()
        {
            RemoveSelection();
            Stroke = Brushes.Blue;
            StrokeThickness = 2;
            IsSelected = true;
        }
        public void RemoveSelection()
        {
            Stroke = currentStroke;
            StrokeThickness = 1;
            IsSelected = false;
        }
    }
}

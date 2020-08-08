using BusinessLogicLayer.ShapeManager;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace BusinessLogicLayer
{
    /// <summary>
    /// Interaction logic for DropThumb.xaml
    /// </summary>
    public partial class DropThumb : Thumb
    {
        private bool _dragStarted;
        //private Point initialMousePosition;
        private readonly UIElement _dropTarget;

        public DropThumb(ShapeBase droppedItem, UIElement dropTarget)
        {
            InitializeComponent();
            this.DataContext = this;
            Shape = droppedItem;
            _dropTarget = dropTarget;

            DragStarted += this.DropThumb_DragStarted;
            DragDelta += this.DropThumb_DragDelta;
            DragCompleted += DropThumb_DragCompleted;

            PreviewMouseLeftButtonUp += DropThumb_PreviewMouseLeftButtonUp;
        }

        private void DropThumb_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_dragStarted) return;
            var canvas = VisualTreeHelper.GetParent((DropThumb)DataContext) as DrawingArea;
            foreach (var thumb in canvas.SelectedItems)
            {
                thumb.Shape.RemoveSelection();
            }

            var sourceItem = e.Source as DropThumb;
            sourceItem.Shape.AddSelection();
        }

        private void DropThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            var dropTargetMousePosition = System.Windows.Input.Mouse.GetPosition(_dropTarget);
        }

        private void DropThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            _dragStarted = true;
            var sourceItem = e.Source as DropThumb;
            if (!sourceItem.Shape.IsSelected) return;
            var canvas = VisualTreeHelper.GetParent(sourceItem) as DrawingArea;
            var mousePosition = Mouse.GetPosition(canvas);

            foreach (var thumb in canvas.SelectedItems)
            {
                //var a = Mouse.GetPosition(thumb);
                var thumbLeft = Canvas.GetLeft(thumb);
                var thumbTop = Canvas.GetTop(thumb);

                if ((thumbLeft + e.HorizontalChange) > 0 && (thumbLeft + thumb.ActualWidth + e.HorizontalChange) < canvas.ActualWidth)
                {
                    Canvas.SetLeft(thumb, thumbLeft + e.HorizontalChange);
                }

                if ((thumbTop + e.VerticalChange) > 0 && (thumbTop + thumb.ActualHeight + e.VerticalChange) < canvas.ActualHeight)
                {
                    Canvas.SetTop(thumb, thumbTop + e.VerticalChange);
                }
            }
        }

        private void DropThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            _dragStarted = false;
        }

        public ShapeBase Shape { get; set; }
    }
}

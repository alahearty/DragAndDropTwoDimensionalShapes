using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public static class DoDragDrop
    {
        private static DragData dragData;
        private static DataFormat m_Format = DataFormats.GetDataFormat("CCLTest");

        #region IsDragSource

        public static readonly DependencyProperty IsDragSourceProperty = DependencyProperty.RegisterAttached("IsDragSource", typeof(bool), typeof(DoDragDrop),
                new PropertyMetadata(false, IsDragSourceChanged));

        public static bool GetIsDragSource(UIElement element)
        {
            return (bool)element.GetValue(IsDragSourceProperty);
        }

        public static void SetIsDragSource(UIElement element, bool value)
        {
            element.SetValue(IsDragSourceProperty, value);
        }

        private static void IsDragSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = (UIElement)d;
            if ((bool)e.NewValue)
            {
                uiElement.PreviewMouseLeftButtonDown += DragSource_PreviewMouseLeftButtonDown;
                uiElement.PreviewMouseLeftButtonUp += DragSource_PreviewMouseLeftButtonUp;
                uiElement.MouseMove += DragSource_PreviewMouseMove;
            }
            else
            {
                uiElement.PreviewMouseLeftButtonDown -= DragSource_PreviewMouseLeftButtonDown;
                uiElement.PreviewMouseLeftButtonUp -= DragSource_PreviewMouseLeftButtonUp;
                uiElement.MouseMove -= DragSource_PreviewMouseMove;
            }
        }

        private static void DragSource_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragData = new DragData(sender, e);
            e.Handled = true;
        }

        private static void DragSource_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (dragData != null)
            {
                dragData = null;
            }
        }

        private static void DragSource_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (dragData != null)
            {
                Point dragStart = dragData.DragStartPosition;
                Point position = e.GetPosition(null);

                if (Math.Abs(position.X - dragStart.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - dragStart.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    if (dragData.Data != null)
                    {
                        DataObject data = new DataObject(m_Format.Name, dragData.Data);
                        System.Windows.DragDrop.DoDragDrop(dragData.VisualSource, data, dragData.Effects);
                        dragData = null;
                    }
                }
            }
        }
        #endregion

        #region IsDropTarget

        public static readonly DependencyProperty IsDropTargetProperty = DependencyProperty.RegisterAttached("IsDropTarget", typeof(bool), typeof(DoDragDrop),
                new PropertyMetadata(false, IsDropTargetChanged));

        public static bool GetIsDropTarget(DependencyObject element)
        {
            return (bool)element.GetValue(IsDropTargetProperty);
        }
        public static void SetIsDropTarget(DependencyObject element, bool value)
        {
            element.SetValue(IsDropTargetProperty, value);
        }
        private static void IsDropTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = (UIElement)d;
            if ((bool)e.NewValue)
            {
                uiElement.AllowDrop = true;
                uiElement.PreviewDrop += DropTarget_PreviewDrop;
            }
            else
            {
                uiElement.AllowDrop = false;
                uiElement.PreviewDrop -= DropTarget_PreviewDrop;
            }
        }
        static void DropTarget_PreviewDrop(object sender, DragEventArgs e)
        {
            var dropData = new DropData(sender, e, m_Format.Name);
            var dropTarget = sender as DrawingArea;

            if (dropData.Data != null && dropTarget != null)
            {
                dropTarget.AddShape(dropData.Data);
            }
            e.Handled = true;
        }
        #endregion
    }
}

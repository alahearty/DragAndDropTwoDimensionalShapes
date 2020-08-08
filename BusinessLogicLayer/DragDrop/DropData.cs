using BusinessLogicLayer.ShapeManager;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BusinessLogicLayer
{
    internal class DropData
    {
        internal DropData(object sender, DragEventArgs e, string dataFormat)
        {
            var data = e.Data.GetDataPresent(dataFormat) ? e.Data.GetData(dataFormat) : e.Data;
            SourceItem = (UIElement)data;
            DropTarget = (UIElement)sender;
            DropPosition = e.GetPosition(DropTarget);
            SetData();
        }
        internal UIElement SourceItem { get; }
        internal ShapeBase DroppedItem { get; private set; }
        internal UIElement Data { get; private set; }
        internal Point DropPosition { get; }
        internal UIElement DropTarget { get; }

        private void SetData()
        {
            Type type = SourceItem.GetType();
            DroppedItem = Activator.CreateInstance(type) as ShapeBase;
            DroppedItem.SetToOriginalStroke();
            Data = new DropThumb(DroppedItem, DropTarget);
            Canvas.SetLeft(Data, DropPosition.X);
            Canvas.SetTop(Data, DropPosition.Y);
        }
    }
}

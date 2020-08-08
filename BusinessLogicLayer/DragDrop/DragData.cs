using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BusinessLogicLayer
{
    internal class DragData
    {
        internal DragData(object sender, MouseButtonEventArgs e)
        {
            DragStartPosition = e.GetPosition(null);
            Effects = DragDropEffects.Copy;
            MouseButton = e.ChangedButton;
            VisualSource = sender as UIElement;
            Data = e.OriginalSource;
        }

        internal object Data { get; set; }
        internal Point DragStartPosition { get; private set; }
        internal DragDropEffects Effects { get; set; }
        internal MouseButton MouseButton { get; private set; }
        internal UIElement VisualSource { get; private set; }
    }
}

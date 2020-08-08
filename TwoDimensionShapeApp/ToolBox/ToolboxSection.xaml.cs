using System.Windows.Controls;
using TwoDimensionShapeApp.ToolBox;

namespace TwoDimensionShapeApp.Views
{
    /// <summary>
    /// Interaction logic for ToolboxSection.xaml
    /// </summary>
    public partial class ToolboxSection : UserControl
    {
        public ToolboxSection()
        {
            InitializeComponent();
            DataContext = new ToolBoxViewModel();
        }
    }
}

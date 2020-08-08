using System.Windows.Controls;
using TwoDimensionShapeApp.ToolBar;

namespace TwoDimensionShapeApp.Views
{
    public partial class ToolbarSection : UserControl
    {
        public ToolbarSection()
        {
            InitializeComponent();
            DataContext = new ToolBarViewModel();
        }
    }
}

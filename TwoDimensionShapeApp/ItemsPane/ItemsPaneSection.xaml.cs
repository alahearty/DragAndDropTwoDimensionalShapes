using System.Windows.Controls;
using TwoDimensionShapeApp.ItemsPane;

namespace TwoDimensionShapeApp.Views
{
    /// <summary>
    /// Interaction logic for ItemsPaneSection.xaml
    /// </summary>
    public partial class ItemsPaneSection : UserControl
    {
        public ItemsPaneSection()
        {
            InitializeComponent();
            DataContext = new ItemsPaneViewModel();
        }
    }
}

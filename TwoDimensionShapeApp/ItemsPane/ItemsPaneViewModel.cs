using BusinessLogicLayer.Util;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Shapes;
using TwoDimensionShapeApp.ToolBox;

namespace TwoDimensionShapeApp.ItemsPane
{
    public class ItemsPaneViewModel 
    {
        private readonly IEventAggregator eventAggregator;

        public ItemsPaneViewModel()
        {
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            eventAggregator.GetEvent<ItemsPaneEvent>().Subscribe(AddItem);
            eventAggregator.GetEvent<ClearEvent>().Subscribe(Clear);
        }

        private void Clear()
        {
           Shapes.Clear();
        }
        private void AddItem(string name)
        {
            var shape = Shapes.FirstOrDefault(x => x.Name.Equals(name));
            if(shape == null)
            {
                var toolBox = new ToolBoxViewModel();
                shape = toolBox.Shapes.FirstOrDefault(x => x.Name.Equals(name));
                Shapes.Add(shape);
            }
        }

        public ObservableCollection<Shape> Shapes { get; } = new ObservableCollection<Shape>();
    }
}

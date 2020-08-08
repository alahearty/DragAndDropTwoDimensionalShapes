using BusinessLogicLayer.Util;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Media;

namespace TwoDimensionShapeApp.ToolBar
{
    public class ToolBarViewModel : BindableBase
    {
        private IEventAggregator eventAggregator;
        public ToolBarViewModel()
        {
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            SaveCommand = new DelegateCommand(Save);
            LoadCommand = new DelegateCommand(Load);
            DeleteCommand = new DelegateCommand(Delete);
            ClearCommand = new DelegateCommand(Clear);
            eventAggregator.GetEvent<SaveNotifier>().Subscribe(SaveDeleteEnabler);
            eventAggregator.GetEvent<ReturnNotifier>().Subscribe(DisplayMessage);

        }

        private void DisplayMessage(Model obj)
        {
            if (obj.Status == true && obj.Acion == "Delete")
            {
                MessageBox.Show("Shapes Deleted Successfully");
            }
            else if (obj.Status == true && obj.Acion == "Save")
            {
                MessageBox.Show("Shapes Saved Successfully");
            }

        }

        private void SaveDeleteEnabler(bool obj)
        {
            if (obj == true)
                IsEnabled = obj;
            else
                IsEnabled = false;
        }
        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); RaisePropertyChanged(); }
        }

        private string criterium = "None";
        public string Criterium
        {
            get => criterium;
            set
            {
                criterium = value;
                RaisePropertyChanged();
                eventAggregator.GetEvent<CriteriaEvent>().Publish(Criterium);
            }
        }

        public string[] Criteria => new string[] { "None", "Select First Item", "Select Last Item", "Select Middle Item", "Select All Item" };


        private Color strokeColor;
        public Color StrokeColor
        {
            get => strokeColor;
            set
            {
                strokeColor = value;
                RaisePropertyChanged();
                eventAggregator.GetEvent<StrokeColorEvent>().Publish(StrokeColor.ToString());
            }
        }

        private Color fillColor = Color.FromArgb(223, 234, 230, 10);
        public Color FillColor
        {
            get => fillColor;
            set
            {
                fillColor = value;
                RaisePropertyChanged();

                eventAggregator.GetEvent<FillColorEvent>().Publish(FillColor.ToString());
            }
        }

        public DelegateCommand SaveCommand { get; private set; }
        private void Save()
        {
            eventAggregator.GetEvent<SaveEvent>().Publish();
        }
        public DelegateCommand LoadCommand { get; private set; }
        private void Load()
        {
            eventAggregator.GetEvent<LoadEvent>().Publish();
        }
        public DelegateCommand ClearCommand { get; private set; }
        private void Clear()
        {
            eventAggregator.GetEvent<ClearEvent>().Publish();
        }
        public DelegateCommand DeleteCommand { get; private set; }
        private void Delete()
        {
            eventAggregator.GetEvent<DeleteEvent>().Publish();
            Clear();
        }
    }
}

using Prism.Regions;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace TwoDimensionShapeApp
{
    public class RegisterRegion : IModule
    {
        private IRegionManager _regionManager;
        public RegisterRegion(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
        }
        public void Initialize()
        {

            _regionManager.RegisterViewWithRegion("HeaderSection", typeof(Views.HeaderSection));
            _regionManager.RegisterViewWithRegion("ToolbarSection", typeof(Views.ToolbarSection));
            _regionManager.RegisterViewWithRegion("ToolboxSection", typeof(Views.ToolboxSection));
            _regionManager.RegisterViewWithRegion("DrawingCanvasSection", typeof(Views.DrawingCanvasSection));
            _regionManager.RegisterViewWithRegion("ItemsPaneSection", typeof(Views.ItemsPaneSection));
            _regionManager.RegisterViewWithRegion("FooterSection", typeof(Views.FooterSection));
        }
    }

}
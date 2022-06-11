using BusinessLogicLayer.ShapeManager;
using BusinessLogicLayer.Util;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace BusinessLogicLayer
{
    public class DrawingArea : Canvas
    {
        private readonly IEventAggregator eventAggregator;
        private readonly ShapeRepository repo;
        public DrawingArea()
        {
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            eventAggregator.GetEvent<StrokeColorEvent>().Subscribe(ChangeSelectedStrokeColor);
            eventAggregator.GetEvent<FillColorEvent>().Subscribe(ChangeSelectedFillColor);
            eventAggregator.GetEvent<CriteriaEvent>().Subscribe(SelectItem);
            eventAggregator.GetEvent<SaveEvent>().Subscribe(SaveOrUpdate);
            eventAggregator.GetEvent<LoadEvent>().Subscribe(Load);
            eventAggregator.GetEvent<DeleteEvent>().Subscribe(Delete);
            eventAggregator.GetEvent<ClearEvent>().Subscribe(ClearCanvas);

            repo = ServiceLocator.Current.GetInstance<ShapeRepository>();

            PreviewMouseLeftButtonDown += DrawingArea_PreviewMouseLeftButtonDown;
            // PreviewDrop += DrawingArea_PreviewDrop;

        }

        private void DrawingArea_PreviewDrop(object sender, DragEventArgs e)
        {
            var count = this.Children.OfType<DropThumb>().Count();
            if (count > 0)
                eventAggregator.GetEvent<SaveNotifier>().Publish(true);
            else
                eventAggregator.GetEvent<SaveNotifier>().Publish(false);
        }

        public void AddShape(UIElement droppedData)
        {
            var thumb = droppedData as DropThumb;
            var lastItemId = this.Children.Count == 0 ? 0 : this.Children.OfType<DropThumb>().OrderBy(x => x.Shape.Identity).Last().Shape.Identity;
            thumb.Shape.Identity = lastItemId + 1;
            this.Children.Add(droppedData);
            eventAggregator.GetEvent<ItemsPaneEvent>().Publish(thumb.Shape.Name);
            eventAggregator.GetEvent<SaveNotifier>().Publish(true);
        }

        private void ChangeSelectedStrokeColor(string color)
        {
            ChangeSelectedColor((shape) =>
            {
                var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
                shape.SetCurrentStroke(brush);
            });
        }

        private void ChangeSelectedFillColor(string color)
        {
            ChangeSelectedColor((shape) =>
            {
                var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
                shape.Fill = brush;
            });
        }

        private void ChangeSelectedColor(Action<ShapeBase> action)
        {
            foreach (DropThumb thumb in this.Children)
            {
                var shape = thumb.Shape;
                if (shape.IsSelected)
                {
                    action.Invoke(shape);
                }
            }
        }
        private void SelectItem(string criterium)
        {
            if (this.Children.Count == 0) return;

            RemoveSelection();

            if (criterium == "Select First Item")
            {
                var firstItem = this.Children.OfType<DropThumb>().FirstOrDefault(x => x.Shape.Identity == 1);
                if (firstItem != null)
                {
                    firstItem.Shape.AddSelection();
                }
            }
            else if (criterium == "Select Last Item")
            {
                var firstItem = this.Children.OfType<DropThumb>().FirstOrDefault(x => x.Shape.Identity == this.Children.Count);
                if (firstItem != null)
                {
                    firstItem.Shape.AddSelection();
                }
            }
            else if (criterium == "Select Middle Item")
            {
                var shapes = this.Children.OfType<DropThumb>().OrderBy(x => x.Shape.Identity);
                var mod = shapes.Count() % 2;
                var result = Math.Truncate(shapes.Count() / 2.0);
                IEnumerable<DropThumb> middleShapes;
                if (mod == 0)
                    middleShapes = shapes.Skip((int)result - 1).Take(2);
                else
                    middleShapes = shapes.Skip((int)result).Take(1);

                foreach (var thumb in middleShapes)
                    thumb.Shape.AddSelection();
            }
            else if (criterium == "Select All Item")
            {
                var shapes = this.Children.OfType<DropThumb>();
                foreach (var thumb in shapes)
                    thumb.Shape.AddSelection();
            }
        }

        private void DrawingArea_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.Source is DrawingArea)
                RemoveSelection();
        }

        private void RemoveSelection()
        {
            if (this.Children.Count > 0)
            {
                foreach (DropThumb thumb in this.Children)
                {
                    thumb.Shape.RemoveSelection();
                }
            }
        }
        //Code to Get Effect changes 
        public IEnumerable<DropThumb> SelectedItems => this.Children.OfType<DropThumb>().Where(x => x.Shape.IsSelected);
        public int CanvasChildrenCount => Children.OfType<DropThumb>().ToList().Count;

        private void SaveOrUpdate()
        {
            bool status = false;
            try
            {
                foreach (var thumb in this.Children.OfType<DropThumb>())
                {
                    var shape = thumb.Shape;

                    if (shape.Id == Guid.Empty)
                    {
                        shape.Id = Guid.NewGuid();
                        var query = "INSERT INTO CanvasShape_tbl (Id,Width,Height,Stroke,Fill,IsSelected,ShapeLeft,ShapeTop,ShapeName) values (@Id,@Width,@Height,@Stroke,@Fill,@IsSelected,@ShapeLeft,@ShapeTop,@ShapeName)";
                        var values = new Dictionary<string, object>
                        {
                            { "Id", shape.Id.ToString() },
                            { "Width", shape.Width },
                            { "Height", shape.Height },
                            { "Stroke", shape.Stroke.ToString() },
                            { "Fill", shape.Fill.ToString() },
                            { "IsSelected", shape.IsSelected },
                            { "ShapeLeft", GetLeft(thumb) },
                            { "ShapeTop", GetTop(thumb) },
                            { "ShapeName", shape.Name }
                        };
                        repo.Add(query, values);

                    }
                    else
                    {
                        // Update
                        var query = "UPDATE CanvasShape_tbl SET Width = ?, Height =?, Stroke = ?, Fill = ?, IsSelected =?, ShapeLeft =?, ShapeTop = ? ,ShapeName = ? where Id = ?";

                        var values = new Dictionary<string, object>
                        {
                            { "Width", shape.Width },
                            { "Height", shape.Height },
                            { "Stroke", shape.Stroke.ToString() },
                            { "Fill", shape.Fill.ToString() },
                            { "IsSelected", shape.IsSelected },
                            { "ShapeLeft", GetLeft(thumb) },
                            { "ShapeTop", GetTop(thumb) },
                            { "ShapeName", shape.Name },
                            { "Id", shape.Id.ToString() },
                        };

                        repo.Update(query, values);
                    }

                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
                throw;
            }
            var data = new Model()
            {
                Status = status,
                Acion = "Save"
            };
            eventAggregator.GetEvent<ReturnNotifier>().Publish(data);
        }
        private void Load()
        {
            ClearCanvas();
            var shapes = repo.GetList().ToList();
            foreach (var shape in shapes)
            {
                var thumb = new DropThumb(shape, this);
                SetLeft(thumb, shape.Left);
                SetTop(thumb, shape.Top);
                AddShape(thumb);
            }
        }

        private void ClearCanvas()
        {
            Children.Clear();
        }

        private void Delete()
        {
            bool status = false;

            try
            {
                foreach (var thumb in this.Children.OfType<DropThumb>())
                {
                    var shape = thumb.Shape;

                    if (shape.Id != Guid.Empty && shape.IsSelected)
                    {
                        var query = "DELETE FROM CanvasShape_tbl where Id = ?";
                        var values = new Dictionary<string, object>
                        {
                            { "Id", shape.Id.ToString() }
                        };
                        repo.Delete(query, values);
                    }
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
                throw;
            }
            var data = new Model()
            {
                Status = status,
                Acion = "Delete"
            };
            eventAggregator.GetEvent<ReturnNotifier>().Publish(data);
        }

        private ICommand refreshAll;
        public ICommand RefreshAll
        {
            get
            {
                if (refreshAll == null)
                {
                    refreshAll = new DelegateCommand(delegate ()
                    {
                        Children.Clear();
                    });
                }
                return refreshAll;
            }
        }
    }
}

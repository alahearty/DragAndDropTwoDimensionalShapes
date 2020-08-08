using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Media;

namespace BusinessLogicLayer.ShapeManager
{
    public class ShapeRepository : Repository<ShapeBase>
    {
        public IList<ShapeBase> GetList()
        {
            var items = new List<ShapeBase>();
            var cmd = new SqlCommand("select * from CanvasShape_tbl", connection);

            var reader = cmd.ExecuteReader();
            try
            {

                while (reader.Read())
                {
                    var name = reader["ShapeName"].ToString();
                    var shape = Shape(name);
                    if (shape != null)
                    {
                        shape.Width = Convert.ToDouble(reader["Width"]);
                        shape.Height = Convert.ToDouble(reader["Height"]);
                        shape.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(reader["Stroke"].ToString()));
                        shape.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(reader["Fill"].ToString()));
                        shape.IsSelected = Convert.ToBoolean(reader["IsSelected"]);
                        shape.Left = Convert.ToDouble(reader["ShapeLeft"]);
                        shape.Top = Convert.ToDouble(reader["ShapeTop"]);
                        shape.Id = Guid.Parse(reader["Id"].ToString());
                        items.Add(shape);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                reader.Close();
            }
            return items;
        }
        private ShapeBase Shape(string name)
        {
            ShapeBase shape = null;
            if (name == "Circle")
            {
                return new Circle();
            }
            else if (name == "Hexagon")
            {
                return new Hexagon();
            }
            else if (name == "Quadrilateral")
            {
                return new Quadrilateral();
            }
            else if (name == "Cross")
            {
                return new Cross();
            }
            else if (name == "Pentagon")
            {
                return new Pentagon();
            }
            else if (name == "Line")
            {
                return new Line();
            }
            else if (name == "Square")
            {
                return new Square();
            }
            else if (name == "Trapezium")
            {
                return new Trapezium();
            }
            else if (name == "Star")
            {
                return new Star();
            }
            else if (name == "CurveArc")
            {
                return new CurveArc();
            }
            return shape;
        }
    }
}

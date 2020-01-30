using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures;
using System;
using System.Collections.Generic;


namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            var handler = new DrawingHandler();
            var selection = handler.GetDrawingObjectSelector().GetSelected();
            
			
            PointList pointlist = new PointList();
            List<double> X = new List<double>();
            List<double> Y = new List<double>();

            while(selection.MoveNext())
            {
                if (selection.Current is Polygon)
                {
					Polygon polygon = selection.Current as Polygon;
                    pointlist = polygon.Points;

                    foreach(Point i in pointlist)
                    {

                        X.Add(i.X);
                        Y.Add(i.Y);

                    }
                   
                }
				
            }
            int n = X.Count;

            double area = polygonArea(X,Y,n)/1000000;

            string msg = String.Format("Polygon area is: {0} m2", Math.Round(area,3));

            System.Windows.Forms.MessageBox.Show(msg);           

        }

        public static double polygonArea(List<double> X, List<double> Y, int n)
        {
            double area = 0.0;
            int j = n - 1;
            for (int i = 0; i < n; i++) 
            { 
                area += (X[j] + X[i]) * (Y[j] - Y[i]); 
                j = i;
            }
            return Math.Abs(area/2.0);
        }
    }

   
}
using System;
using System.Collections;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Geometry3d;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
       public static void Run(Tekla.Technology.Akit.IScript akit)
	   {   
			ModelViewEnumerator ViewEnum = ViewHandler.GetVisibleViews();
			ViewEnum.MoveNext();
			View ActiveView = ViewEnum.Current;
			ClipPlaneCollection ClipPlanes = ActiveView.GetClipPlanes();
			if (ClipPlanes.Count > 0)
			{
				IEnumerator PlaneEnum = ClipPlanes.GetEnumerator();
				while (PlaneEnum.MoveNext())
				{
					ClipPlane CPlane = PlaneEnum.Current as ClipPlane;
					if (CPlane != null)
					{
						CPlane.Delete();
					}
				}
			}	
	   }	
    }
}

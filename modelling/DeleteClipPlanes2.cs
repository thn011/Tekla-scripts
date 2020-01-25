using System;
using System.Collections;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Geometry3d;

/* this is just a little modification of original code from TeklaOpenApi Reference
original code does not work if you have open two views and only one of them has clipplanes added
now it deletes all clip planes on all views
*/
namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
       public static void Run(Tekla.Technology.Akit.IScript akit)
	   {   
			ModelViewEnumerator ViewEnum = ViewHandler.GetVisibleViews();
			while(ViewEnum.MoveNext());
			{
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
}

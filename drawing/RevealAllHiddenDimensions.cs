#pragma warning disable 1633 // Unrecognized #pragma directive
#pragma reference "Tekla.Macros.Wpf.Runtime"
#pragma reference "Tekla.Macros.Akit"
#pragma reference "Tekla.Macros.Runtime"
#pragma warning restore 1633 // Unrecognized #pragma directive

using Tekla.Structures.Drawing;
using Tekla.Structures;


namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            //This will reveall objects in all views in drawing
			
			DrawingHandler DrawingHandler = new DrawingHandler();
			Drawing CurrentDrawing = DrawingHandler.GetActiveDrawing();
			DrawingObjectEnumerator dimensions = CurrentDrawing.GetSheet().GetAllObjects(typeof(ReinforcementBase));
			DrawingHandler.SetMessageExecutionStatus(DrawingHandler.MessageExecutionModeEnum.BY_COMMIT);

            foreach (ReinforcementBase reinf in dimensions)
			{
				if (reinf.Hideable.IsHidden)
				{
					//dim.Delete();
					reinf.Hideable.ShowInDrawingView();
					reinf.Modify();
				}
					
			}
			DrawingHandler.SetMessageExecutionStatus(DrawingHandler.MessageExecutionModeEnum.INSTANT);
			DrawingHandler.GetActiveDrawing().CommitChanges();	
			Tekla.Structures.Model.Operations.Operation.DisplayPrompt("Operation finished");
        }
    }
  
}
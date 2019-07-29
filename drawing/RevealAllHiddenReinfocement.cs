#pragma warning disable 1633 // Unrecognized #pragma directive
#pragma reference "Tekla.Macros.Wpf.Runtime"
#pragma reference "Tekla.Macros.Akit"
#pragma reference "Tekla.Macros.Runtime"
#pragma warning restore 1633 // Unrecognized #pragma directive

using Tekla.Structures.Drawing;
using Tekla.Structures;
using Tekla.Structures.Drawing.UI;
using Tekla.Structures.Geometry3d;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            //Idea is to get only objects in selected view
			ViewBase view;
			DrawingObject dob;
			
			var handler = new DrawingHandler();
            Picker picker = handler.GetPicker();
			picker.PickObject("Select any object in intended view", out dob, out view);
			
			var reinforcement = view.GetAllObjects(typeof(ReinforcementBase));
			
            DrawingHandler.SetMessageExecutionStatus(DrawingHandler.MessageExecutionModeEnum.BY_COMMIT);
			
			foreach (ReinforcementBase reinf in reinforcement)
				{
					if (reinf.Hideable.IsHidden)
					{
					
						reinf.Hideable.ShowInDrawingView();
						reinf.Modify();
					}
				}
			DrawingHandler.SetMessageExecutionStatus(DrawingHandler.MessageExecutionModeEnum.INSTANT);
			handler.GetActiveDrawing().CommitChanges();
			Tekla.Structures.Model.Operations.Operation.DisplayPrompt("Operation finished");
        }
    }

   
}
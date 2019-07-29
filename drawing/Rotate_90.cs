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
            var handler = new DrawingHandler();
            var selection = handler.GetDrawingObjectSelector().GetSelected();
            
			DrawingHandler.SetMessageExecutionStatus(DrawingHandler.MessageExecutionModeEnum.BY_COMMIT);
			
            double rotang = 90;

            while(selection.MoveNext())
            {
                if (selection.Current is MarkBase)
                {
					MarkBase mark = selection.Current as MarkBase;
					mark.Attributes.RotationAngle = rotang;
					mark.Attributes.PlacingAttributes.IsFixed = true;
					mark.Modify();
                }
				else
				{
					Text text = selection.Current as Text;
					text.Attributes.Angle = rotang;
					text.Attributes.PlacingAttributes.IsFixed = true;
					text.Modify();
				}
            }
			
			DrawingHandler.SetMessageExecutionStatus(DrawingHandler.MessageExecutionModeEnum.INSTANT);
			handler.GetActiveDrawing().CommitChanges();
        }
    }

   
}
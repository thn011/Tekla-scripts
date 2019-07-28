using System.Windows.Forms;
using System;
using System.Collections.Generic;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {

            ModelObjectEnumerator moe = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();
            List<string> guids = new List<string>();

            foreach (ModelObject m in moe)
            {
                if (m != null)
                {
                    guids.Add(m.Identifier.GUID.ToString());
                }
            }


            if (guids.Count < 1)
            {
                Tekla.Structures.Model.Operations.Operation.DisplayPrompt("Select object");
            }
            else
            {
                string Guid_To_Clipboard = string.Join(" ", guids);

                //MessageBox.Show(Guid_To_Clipboard);
                Clipboard.SetText(Guid_To_Clipboard.ToUpper());
                Tekla.Structures.Model.Operations.Operation.DisplayPrompt("GUID was copied to your clipboard");

            }
        }
    }
}

using System.Windows.Forms;
using System;
using System.Collections.Generic;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using System.IO;


namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            var reportName = "ReportFromSelected";
            var reportFolder = string.Empty;
            Tekla.Structures.TeklaStructuresSettings.GetAdvancedOption("XS_REPORT_OUTPUT_DIRECTORY", ref reportFolder);
            var path = new Model().GetInfo().ModelPath + reportFolder.Replace(".\\", "\\") + "\\" + reportName + "_" + Environment.UserName + ".xsr"; 

            ModelObjectEnumerator moe = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();
            
            using (System.IO.StreamWriter sw = new StreamWriter(path))
            {
                    
                foreach (ModelObject m in moe)
                {
                    if (m != null)
                    {
                   
                        sw.WriteLine(string.Format("guid: {0}", m.Identifier.GUID.ToString().ToUpper()));
                    }
                }
            }   
            Tekla.Structures.Model.Operations.Operation.DisplayPrompt("Operation finished");
            Tekla.Structures.Model.Operations.Operation.DisplayReport(path);
            
        }
    }
}

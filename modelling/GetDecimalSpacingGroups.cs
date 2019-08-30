using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using System;
using System.IO;

namespace Tekla.Technology.Akit.UserScript
{
    public class Script
    {
        public static void Run(Tekla.Technology.Akit.IScript akit)
        {
            /*
            The main task for this script is to find reinforcement groups with nasty spacing values like 200.15 or 150.48. Tekla rounds up or down some small accurate numbers,
            the key is to found such interval to make this script work.
             */
            var reportName = "DecimalSpacingGroups";
            var reportFolder = string.Empty;
            Tekla.Structures.TeklaStructuresSettings.GetAdvancedOption("XS_REPORT_OUTPUT_DIRECTORY", ref reportFolder);
            var path = new Model().GetInfo().ModelPath + reportFolder.Replace(".\\", "\\") + "\\" + reportName + "_" + Environment.UserName + ".xsr"; 
            ModelObjectEnumerator moe = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();
            System.Collections.ArrayList decimalgroups = new System.Collections.ArrayList();
            var pb = new Tekla.Structures.Model.Operations.Operation.ProgressBar();
            int i = 0;
			foreach (BaseRebarGroup m in moe)
            {  
			   pb.Display(100, "Processing data", "Please wait", "Cancel", "0%");
                
                if (m != null)
                {
                    foreach (double spacing in m.Spacings)
                    {
                        if (spacing%1>0.009) // this should be changed like: >0.009999999999999999 & <0.999999999999999999 but it needs some tests
                        {
                            decimalgroups.Add(m);
                        }
                    }
                }
                var percentage = (i++) * 100 / moe.GetSize();
                pb.SetProgress(percentage.ToString() + "%", percentage); 
            } 
            pb.Close();        
            using (System.IO.StreamWriter sw = new StreamWriter(path))
            {
                foreach (ModelObject m in decimalgroups)
                {
                    sw.WriteLine(string.Format("guid: {0}", m.Identifier.GUID.ToString().ToUpper()));
                }
            }
            Tekla.Structures.Model.Operations.Operation.DisplayPrompt("Operation finished");
            Tekla.Structures.Model.Operations.Operation.DisplayReport(path);
            
        }
    }
}

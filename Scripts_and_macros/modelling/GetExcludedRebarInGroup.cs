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
            System.Collections.ArrayList hiddenFirsts = new System.Collections.ArrayList();
            foreach (ModelObject m in moe)
            {
                if (m != null && m is RebarGroup && (m as RebarGroup).ExcludeType != RebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_NONE)
                {
                    hiddenFirsts.Add(m);
                }
            }
            new Tekla.Structures.Model.UI.ModelObjectSelector().Select(hiddenFirsts);
        }
    }
}

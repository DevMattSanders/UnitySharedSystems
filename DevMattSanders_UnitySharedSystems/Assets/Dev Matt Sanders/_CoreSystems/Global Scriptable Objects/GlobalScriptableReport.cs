#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace DevMattSanders._CoreSystems
{
    public class GlobalScriptableReport : OdinEditorWindow
    {


        [MenuItem("Project/Global Scriptable Report")]
        public static void OpenWindow()
        {
            GetWindow<GlobalScriptableReport>().Show();

            ScriptableObjectReport();
        }

        public static void GenerateReportStatic()
        {
            GetWindow<GlobalScriptableReport>()?.GenerateReport(); ;
        }

        [Button]
        public void GenerateReport()
        {
            ScriptableObjectReport();
        }

        public static void ScriptableObjectReport()
        {
            GlobalScriptableCollection.instance.GetAllMonoScriptableObjects();
            int disabledCounter = 0;
            string disabledGso = "";

            GlobalScriptableReport window = GetWindow<GlobalScriptableReport>();

            window.enabledSystems.Clear();
            window.disabledSystems.Clear();


            foreach (GlobalScriptable next in GlobalScriptableCollection.instance.allGlobalScriptables)
            {
                if (next.enabled)
                {
                    window.enabledSystems.Add(next);
                }
                else
                {
                    window.disabledSystems.Add(next);
                    disabledCounter++;
                    disabledGso += next.name + "\n";
                }
            }
        }

        [ReadOnly]
        public List<GlobalScriptable> enabledSystems = new List<GlobalScriptable>();
        [ReadOnly]
        public List<GlobalScriptable> disabledSystems = new List<GlobalScriptable>();


    }
}
#endif

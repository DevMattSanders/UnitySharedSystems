using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
#endif

namespace DevMattSanders._CoreSystems
{
    public class GlobalScriptableCollection : ScriptableObject
    {
        [InlineButton("GetAllMonoScriptableObjects",label:"Refresh")]
        public GlobalScriptable[] allGlobalScriptables;

        public static UnityEvent onEditorHeartbeat = new UnityEvent();

#if UNITY_EDITOR
    public static GlobalScriptableCollection instance;

    private void OnEnable()
    {
     //   Debug.Log("Restarted Editor Update Loop");
        instance = this;
        EditorApplication.update += RunEditorUpdate;
    }

    private void OnDisable()
    {
        EditorApplication.update -= RunEditorUpdate;
    }

    public int callEditorUpdateEveryXFrames = 6;
    int counter = 0;
    private void RunEditorUpdate()
    {
        if (Application.isPlaying) return;

        counter++;
        if (counter >= callEditorUpdateEveryXFrames)
        {
            counter = 0;

            foreach (GlobalScriptable next in allGlobalScriptables)
            {
                if (next.enabled == false && next.AllowDisabling())
                {
                    continue;                   
                }
                else
                {
                    next.EditorUpdate();
                }
            }

            onEditorHeartbeat.Invoke();
        }
    }


    [MenuItem("Project/Scriptable Object Report")]
    public void ScriptableObjectReport()
    {
        GetAllMonoScriptableObjects();
        int disabledCounter = 0;
        string disabledGso = "";
        foreach (GlobalScriptable next in allGlobalScriptables)
        {
            if (next.enabled == false)
            {
                disabledCounter++;
                disabledGso += next.name + "\n";
            }
        }

        Debug.Log(
            "--SCRIPTABLE OBJECT REPORT--"
            + "\n"
            + "Total Systems = " + allGlobalScriptables.Length
              + "\n"
              + "Total Disabled = " + disabledCounter
                + "\n"
                + "Disabled List:\n" + disabledGso);
    }

    [PropertyOrder(0)]
    
    public void GetAllMonoScriptableObjects()
    {
        allGlobalScriptables = GetAllInstances<GlobalScriptable>();
    }

    public static T[] GetAllInstances<T>() where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)         //probably could get optimized
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;
    }

#endif
    }
}

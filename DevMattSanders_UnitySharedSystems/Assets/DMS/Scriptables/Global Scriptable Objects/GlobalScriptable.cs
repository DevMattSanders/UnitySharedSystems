using UnityEngine;
#if UNITY_EDITOR
#endif


namespace DMS.Scriptables
{
    public class GlobalScriptable : ScriptableObject
    {
        #region Enabled
        [HideInInspector]
        public bool enabled = true;

#if UnityEditor
    [HorizontalGroup("Enabled Button", width: 65)]
    [ShowIf("ShowEnabled")]
    [Button(name: "Disabled"), GUIColor(1, 0.2f, 0), PropertyOrder(-1)]
    public void SetEnabled()
    {
	    enabled = true;
        
	    GlobalScriptableReport.GenerateReportStatic();
       
    }

	
    private bool ShowEnabled()
    {
        if (AllowDisabling() && enabled == false) return true;

        return false;
    }

    [HorizontalGroup("Enabled Button", width: 65)]
    [ShowIf("ShowDisabled")]
    [Button(name: "Enabled"), GUIColor(0, 1, 0), PropertyOrder(-1)]
    public void SetDisabled()
    {

        enabled = false;
        GlobalScriptableReport.GenerateReportStatic();
    }

    private bool ShowDisabled()
    {
        if (AllowDisabling() && enabled) return true;

        return false;
    }
#endif
        public virtual bool AllowDisabling()
        {
            return true;
        }
 
        #endregion

        public virtual void SoSetStartingValue()
        {
            if(enabled == false && AllowDisabling() == false)
            {
                Debug.Log("This Gso (" + name + ") is disabled even though it is not allowed! Setting to enabled");
                enabled = true;
            }
        }

        public virtual void SoStart()
        {

        }

        public virtual void SoEnd()
        {
        }

        public virtual void Save()
        {

        }

        public virtual void Load()
        {
        }

#if UNITY_EDITOR
    public virtual void EditorUpdate()
    {

    }

#endif

        public virtual void OnEnable()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }
    }
}

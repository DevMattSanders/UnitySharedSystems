using Sirenix.OdinInspector;
using UnityEngine;

namespace DMS.SceneManagement
{
    [System.Serializable]
    public class SceneReference
    {
#if UNITY_EDITOR
    [OnValueChanged("SceneChanged")]
    [SerializeField] private Object scene;    

    public Object SceneObjectWhichShouldOnlyBeAccessedInEditor
    {
        get
        {
            return scene;
        }
    }

    private void SceneChanged(Object _scene)
    {
        if (_scene != null)
        {
            SceneName = _scene.name;
        }
        else
        {
            SceneName = "NO_SCENE";
        }
    }
#endif

        // private string sceneName = "NO_SCENE";

        //  [ReadOnly]
        // [ShowInInspector]
        public string SceneName;/*
    {
        get
        {
            return sceneName;
        }
    }
    */
    }
}

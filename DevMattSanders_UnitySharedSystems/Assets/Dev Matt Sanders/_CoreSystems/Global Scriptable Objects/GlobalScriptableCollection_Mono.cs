using Sirenix.OdinInspector;
using UnityEngine;

namespace DevMattSanders._CoreSystems
{
    public class GlobalScriptableCollection_Mono : MonoBehaviour
    {
        public static GlobalScriptableCollection_Mono instance;

        [InlineEditor]
        public GlobalScriptableCollection globalScriptableCollection;

        public bool findScriptableOnAwake;

        public VoidEvent vo_AllScriptableObjectsCollected;

        public void OnCreatedBeforeScene()
        {
            CallStartingRoutine();
        }

        private void Awake()
        {
            instance = this;
        }

        private void OnDestroy()
        {
            for (int i = 0; i < globalScriptableCollection.allGlobalScriptables.Length; i++)
            {
                if (globalScriptableCollection.allGlobalScriptables[i] == null) continue;

                globalScriptableCollection.allGlobalScriptables[i].SoEnd();
            }
        }

        private void CallStartingRoutine()
        {
#if UNITY_EDITOR
        if (findScriptableOnAwake)
        {
            globalScriptableCollection.GetAllMonoScriptableObjects();
        }
#endif

            for (int i = 0; i < globalScriptableCollection.allGlobalScriptables.Length; i++)
            {
                if (globalScriptableCollection.allGlobalScriptables[i] == null) { continue; }
                GlobalScriptable next = globalScriptableCollection.allGlobalScriptables[i]; next.SoSetStartingValue();
            }

            for (int i = 0; i < globalScriptableCollection.allGlobalScriptables.Length; i++)
            {
                if (globalScriptableCollection.allGlobalScriptables[i] == null) { continue; }
                GlobalScriptable next = globalScriptableCollection.allGlobalScriptables[i]; next.Load();
            }

            for (int i = 0; i < globalScriptableCollection.allGlobalScriptables.Length; i++)
            {
                if (globalScriptableCollection.allGlobalScriptables[i] == null) { continue; }
                GlobalScriptable next = globalScriptableCollection.allGlobalScriptables[i]; next.SoStart();
            }

            //  Debug.Log("Load " + Time.timeSinceLevelLoad);
        }
    }
}

using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace DMS.Addressables
{
    public class LoadAddressable : MonoBehaviour
    {
        private AsyncOperationHandle m_SceneHandle = new AsyncOperationHandle();

        public float loadingSlider;
        // public string sceneName;
        public Object scene;
        public bool allowPercent = false;
        [Button]
        public void LoadScene()
        {
        
            m_SceneHandle = UnityEngine.AddressableAssets.Addressables.DownloadDependenciesAsync(scene.name);
            m_SceneHandle.Completed += OnSceneLoaded;
            allowPercent = true;
        }

        private void OnSceneLoaded(AsyncOperationHandle obj)
        {
            if(obj.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Scene loaded!");

           
                UnityEngine.AddressableAssets.Addressables.LoadSceneAsync(scene.name, UnityEngine.SceneManagement.LoadSceneMode.Additive, true).Completed += Completed;
            }
            else
            {
                Debug.Log("Failed");
            }
        }

        private void Completed(AsyncOperationHandle<SceneInstance> obj)
        {
            //ONCE LOADED, ADD TO CURRENT LIST OF LOADED SCENES (TO MAKE SURE THEY ARE UNLOADED PROPERLY)

            Debug.Log("Completed");
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName(scene.name));
            //  throw new System.NotImplementedException();
        }

        private void Update()
        {
            if (allowPercent)
            {
                // if (m_SceneHandle.IsValid())
                loadingSlider = m_SceneHandle.PercentComplete;
            }
            //  loadingSlider = m_SceneHandle.PercentComplete;
        }
    }
}
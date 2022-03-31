using DMS.Extensions;
using UnityEngine;

namespace DMS.SceneManagement
{
    public class FirstScene_ContentLoader : MonoBehaviour
    {
    
        public float delay = 1;
        // public AssetReference sceneAssetToLoad;
        public SceneReference sceneToLoad;

        //  public List<Object> localScenesToAdd = new List<Object>();
        //  public KeepScenesOpen keepScenesOpen;

        void Start()
        {
//#if UNITY_EDITOR
//        Debug.Log("WARNING! You have launched the first scene within the editor! Expect issues such as duplicate first content!");
            // return;
//#endif

            //SceneLoadManager.instance.

            this.CallWithDelay(LoadFirstContent,delay);
        }
    
        private void LoadFirstContent()
        {
            //  foreach(Object next in keepScenesOpen.sceneNames)
            //   {

            //  }

            //if(!SceneLoadManager.instance.currentOpenScenes.Contains("First Scene"))
            // {
            //     SceneLoadManager.instance.currentOpenScenes.Add("First Scene");
            // }

            //SceneLoadManager.instance.LoadScene(sceneAssetToLoad)

            SceneLoadManager.instance.LoadScene(sceneToLoad);
            //sceneToLoad.LoadScene();
        }
    
        /*
    IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delay);	   

        foreach(Object next in localScenesToAdd)
        {
            Addressables.LoadSceneAsync(next.name, LoadSceneMode.Additive,true);
        }
    }
    */
    }
}

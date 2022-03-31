using Sirenix.OdinInspector;
using UnityEngine;

namespace DevMattSanders._CoreSystems
{
    [CreateAssetMenu(menuName = "MattSanders/Scene Management/Scene Loader")]
    public class SoSceneLoader : GlobalScriptable
    {
        [HideLabel,Title("SceneByReference")]
        public SceneReference sceneByReference;

   
        [Button]
        public void LoadScene()
        {
       
            if(sceneByReference != null)
            {
                SceneLoadManager.instance.LoadScene(sceneByReference);
            }
            else
            {
                Debug.Log("No Scene Found");
            }
        }

        /*
    public bool useSceneType = false;
    public bool isAddressableScene;

    public string sceneGroup;

    [Header("Transitioning out of other scene")]
    public float transitionFromOtherSceneDuration = 1f;
    public GameObject transitionOutOfOtherScenePrefab;
    private GameObject transitionOutOfOtherScenePrefabInstance;

    [Header("Transition into this scene")]
    public float transitionIntoThisSceneDuration = 1f;
    public GameObject transitionIntoThisScenePrefab;
    private GameObject transitionIntoThisScenePrefabInstance;

    public override void SoStart()
    {
       
        base.SoStart();
        return;
        //if (SceneLoadManager.instance.openedSceneNames.Contains(name))
        //{
         //   SceneLoadManager.instance.currentOpenScenes.Add(this);
       // }
    }

    [Button]
    public void LoadScene()
    {
      //  SceneLoadManager.instance.LoadScene(this);
    }
       
    //#region TransitionOutOfOtherScene

   // public void CreateTransitionOutOfOtherScene()
   // {
   //     ClearTransitionOut();
  //      transitionOutOfOtherScenePrefabInstance = GameObject.Instantiate(transitionOutOfOtherScenePrefab);
  //  }

   // public void ClearTransitionOut()
   // {
      //  if (transitionOutOfOtherScenePrefabInstance) GameObject.Destroy(transitionOutOfOtherScenePrefabInstance);
   // }

   // #endregion


   // #region TransitionIn
    
   // public void CreateTransitionIn()
    //{
    //    ClearTransitionIn();
    //    transitionIntoThisScenePrefabInstance = GameObject.Instantiate(transitionIntoThisScenePrefab);
    //}

   // public void ClearTransitionIn()
   // {
   //     if (transitionIntoThisScenePrefabInstance) GameObject.Destroy(transitionIntoThisScenePrefabInstance);
   // }
   // #endregion
    */
    }
}

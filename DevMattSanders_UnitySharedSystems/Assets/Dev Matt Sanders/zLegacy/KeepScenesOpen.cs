/*
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace DevMattSanders._CoreSystems
{
#if UNITY_EDITOR
#endif

    [CreateAssetMenu(menuName = "MattSanders/ProjectEssential/KeepScenesOpen")]
    public class KeepScenesOpen : GlobalScriptable
    {
        public Object firstScene;
        [InfoBox("WARNING! Toggling this true will save all open scenes and remove KO scenes!")]
        //[OnValueChanged("CheckToRemoveScenesInEditor")]
        [ReadOnly] public bool testBuildMode;
        public bool allowSingleSceneEditing = false;
        public bool pushScenesToDDOL = true;
        public bool keepScenesOpen = false;
        public static KeepScenesOpen instance;
        // [OnValueChanged("UpdateStrings")]
        public List<SceneReference> sceneObjects = new List<SceneReference>();

        [ReadOnly]
        public List<SceneInstanceProfile> sceneInstances = new List<SceneInstanceProfile>();
*/
        //   [ReadOnly] public List<string> sceneNames = new List<string>();
        /*
    [Button]
    private void UpdateStrings() //Updates the above strings to make this system work in a built version (Scene objects are null in built version)
    {
        sceneNames.Clear();
#if UNITY_EDITOR
        foreach (Object next in sceneObjects)
        {
            sceneNames.Add(next.name);
        }
#endif
    }
    */

        //  private void CheckToRemoveScenesInEditor()
        // {
        //     if (testBuildMode == true)
        //     {
        //        GoToFirstScene();
        //    }
        // }
/*
        [Button]
        private void GoToFirstScene()
        {
#if UNITY_EDITOR
        //if(testBuildMode == true)
       // {
            EditorSceneManager.SaveOpenScenes();

            string path = AssetDatabase.GetAssetPath(firstScene);
            Scene toLoad = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);

           // foreach (Object scene in sceneObjects)
           // {
                
           //     EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByName(scene.name), true);
           // }
      //  }
       // else
       // {
       //     CheckAndLoadScene();
        //}
#endif
        }



        public override void SoSetStartingValue()
        {
            base.SoSetStartingValue();
            instance = this;
            sceneCounter = 0;
            sceneInstances.Clear();
            //scenes.Clear();
            
//#if UNITY_EDITOR
//        UpdateStrings();
//#endif
        
        }
#if UNITY_EDITOR
    private void RemoveKeepOpenScenes()
    {
         foreach (SceneReference scene in sceneObjects)
         {
             EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByName(scene.SceneName), true);
         }
    }
#endif

#if UNITY_EDITOR
    public override void EditorUpdate()
    {
        base.EditorUpdate();

       // if (testBuildMode == false)
       // {
            CheckAndLoadScene();
       // }
    }
#endif
        public override void SoEnd()
        {
            base.SoEnd();
            sceneCounter = 0;
            sceneInstances.Clear();
            //   scenes.Clear();
        }

        public void CheckAndLoadScene()
        {
#if UNITY_EDITOR
        if (Application.isPlaying) return;

        if (keepScenesOpen == false) return;

        if(SceneManager.GetActiveScene() == EditorSceneManager.GetSceneByName(firstScene.name))
        {
            RemoveKeepOpenScenes();
            testBuildMode = true;
            return;
        }
        else
        {
            testBuildMode = false;
        }

        bool anyActive = false;
        if (allowSingleSceneEditing)
        {
            foreach (SceneReference sceneObject in sceneObjects)
            {
                if (SceneManager.GetActiveScene() == EditorSceneManager.GetSceneByName(sceneObject.SceneName))
                {
                    anyActive = true;
                }
            }
        }
        if (anyActive == false)
        {
            foreach (SceneReference sceneObject in sceneObjects)
            {
                if (EditorSceneManager.GetSceneByName(sceneObject.SceneName).IsValid() == false)
                {
                    //    Debug.Log("Auto loading scene: " + sceneObject.name);
                    string path = AssetDatabase.GetAssetPath(sceneObject.SceneObjectWhichShouldOnlyBeAccessedInEditor);
                    Scene toLoad = EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
                }
            }
        }
#endif
        }


        int sceneCounter = 0;
        [Button]
        public override void SoStart()
        {
            base.SoStart();

            List<string> openedSceneNames = new List<string>();
#if UNITY_EDITOR
        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
            openedSceneNames.Add(loadedScenes[i].name);
         //   Debug.Log(loadedScenes[i]);
        }
#endif
            //Debug.Log(openedSceneNames.)

            //  Debug.Log("Keep scenes open 1");
            foreach (SceneReference sceneRef in sceneObjects)
            {


#if UNITY_EDITOR


            // if(SceneManager.GetSceneByName(sceneName). == true || EditorSceneManager.GetSceneByName(sceneName).isLoaded == true)
            //if (SceneInstanceProfile.SceneInstanceContainsSceneName(sceneInstances,sceneName))
          //  Debug.Log("Looking at scene: " + sceneName);

            if(openedSceneNames.Contains(sceneRef.SceneName))
            {
             //   Debug.Log("Trying to load scene that already exists! " + sceneName);
                continue;
            }

            //if(SceneManager.)
#endif
                // sceneInstances.Add(new SceneInstanceProfile(sceneName));
                Addressables.LoadSceneAsync(sceneRef.SceneName, LoadSceneMode.Additive).Completed += KeepScenesOpen_Completed;
            }

            //    Debug.Log("Keep scenes open 2");
        }

        private void KeepScenesOpen_Completed(AsyncOperationHandle<SceneInstance> obj)
        {
            //   Debug.Log("Keep scenes open 3");
#if UNITY_EDITOR
        if (pushScenesToDDOL == false) return;
#endif
            return;

            SceneInstanceProfile profile = SceneInstanceProfile.GetProfileByName(sceneInstances, obj.Result.Scene.name);

            profile.scene = obj.Result.Scene;
            profile.sceneInstance = obj;

            sceneCounter++;
            //   Debug.Log("Keep scenes open 4");
            if (sceneCounter >= sceneObjects.Count)
            {         
                foreach (SceneInstanceProfile sceneInstance in sceneInstances)
                {
                    foreach(GameObject go in sceneInstance.scene.GetRootGameObjects())
                    {
                        // Debug.Log("Set DDOL: " + go.name);
                        DontDestroyOnLoad(go);
                    }
                    Debug.Log("Keep scenes open 5");
                    Addressables.UnloadSceneAsync(sceneInstance.sceneInstance, true).Completed += KeepScenesOpen_Completed1;
                }
            }
        }

        private void KeepScenesOpen_Completed1(AsyncOperationHandle<SceneInstance> obj)
        {
            Debug.Log("Keep scenes open 6");
            Debug.Log(" =+= " +obj.Result);
            //throw new System.NotImplementedException();
        }
    }



    [System.Serializable]
    public class SceneInstanceProfile
    {
    
        public SceneInstanceProfile(string _sceneName, Scene _scene, AsyncOperationHandle<SceneInstance> _sceneInstance)
        {
            sceneName = _sceneName;
            scene = _scene;
            sceneInstance = _sceneInstance;
        }

        public SceneInstanceProfile(string _sceneName) //To be filled in later
        {
            sceneName = _sceneName;
        }

        public string sceneName;
        public Scene scene;
        public AsyncOperationHandle<SceneInstance> sceneInstance;

        public static bool SceneInstanceContainsSceneName(List<SceneInstanceProfile> profiles, string sceneName)
        {
            foreach(SceneInstanceProfile profile in profiles)
            {
                if (profile.sceneName == sceneName) return true;
            }

            return false;
        }

        public static SceneInstanceProfile GetProfileByName(List<SceneInstanceProfile> profiles, string sceneName)
        {
            foreach(SceneInstanceProfile profile in profiles)
            {
                if (profile.sceneName == sceneName) return profile;
            }

            return null;
        }
    }
}
*/
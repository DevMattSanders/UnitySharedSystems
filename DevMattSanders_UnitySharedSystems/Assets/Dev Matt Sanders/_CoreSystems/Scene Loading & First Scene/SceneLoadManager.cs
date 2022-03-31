using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace DevMattSanders._CoreSystems
{
    [CreateAssetMenu(menuName = "MattSanders/ProjectEssential/SceneLoadManager")]
    public class SceneLoadManager : GlobalScriptable
    {
        public static SceneLoadManager instance;
        [SerializeField] private BoolVariable isCurrentlyLoadingScene;
        public FloatVariable percentAmount;
        public BoolVariable showLoadingScreen;
        [ReadOnly, SerializeField] private string currentScene; 
        [ReadOnly, SerializeField] private string sceneToLoadNext;

        public float endOfLoadingScreenDelay = 1f;

        public List<string> currentOpenScenes = new List<string>();

        public SceneReference loadingScene;
        [ReadOnly,SerializeField] private bool addressableSceneOpen = false;

        // public List<string> openedSceneNames = new List<string>();

        public override void SoSetStartingValue()
        {
            base.SoSetStartingValue();
            addressableSceneOpen = false;
            currentScene = null;
            Debug.Log("Clearing Open Scenes");
            currentOpenScenes.Clear();
            //    openedSceneNames.Clear();
            instance = this;

            //  SceneManager.get

            //    List<string> openedSceneNames = new List<string>();
#if UNITY_EDITOR
        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
            currentOpenScenes.Add(loadedScenes[i].name);
        }
#endif
            //   if (openedSceneNames.Contains(scene))


            //   if (SceneManager.GetSceneByBuildIndex(0).IsValid() == false)
            //{
            //    SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
            //  SceneManager.UnloadSceneAsync(0,UnloadSceneOptions.None);
            //}

        }

        public override void SoEnd()
        {
            base.SoEnd();
            currentScene = null;
            addressableSceneOpen = false;
            currentOpenScenes.Clear();
            //openedSceneNames.Clear();
        }

        public void LoadScene(SceneReference scene)
        {
            if (string.IsNullOrEmpty(scene.SceneName)) return;
            if (currentScene == scene.SceneName) return;

            if (currentOpenScenes.Contains(scene.SceneName)) return;

            if (isCurrentlyLoadingScene.Value) return;
            percentAmount.Value = 0;
            isCurrentlyLoadingScene.Value = true;

            sceneToLoadNext = scene.SceneName;

            BeginTransition();
        }
     

        // public AsyncOperationHandle<SceneInstance> currentSceneOperation;
        // public AsyncOperationHandle<SceneInstance> loadingSceneOperation;
        public AsyncOperation operation;
        private void BeginTransition()
        {
            // Debug.Log("Begin Transition");
            // Debug.Log(loadingScene);
            Addressables.LoadSceneAsync(loadingScene.SceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive, true).Completed += LoadingSceneActive;
        }

        private void LoadingSceneActive(AsyncOperationHandle<SceneInstance> _loadingScene)
        {
            // Debug.Log("Loading Scene Active");
            //loadingSceneOperation = _loadingScene;
            showLoadingScreen.Value = true;
            CoroutineMonobehaviour.instance.CallWithDelay(CheckAndRemoveCurrentScene, 0.5f);
        }

        private void CheckAndRemoveCurrentScene()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadingScene.SceneName));

            //REMOVES FIRST SCENE (IF STILL ACTIVE)
            if (SceneManager.GetSceneByBuildIndex(0).IsValid() == true)
            {
                //  SceneManager.UnloadSceneAsync(0,UnloadSceneOptions.None);
            }

            foreach(string next in currentOpenScenes)
            {
                SceneManager.UnloadSceneAsync(next);
            }

            currentOpenScenes.Clear();

            /*
            if (addressableSceneOpen)
        {
            Addressables.UnloadSceneAsync(currentSceneOperation).Completed += LoadInNextScene;
            addressableSceneOpen = false;
        }
        else
        {
            LoadInNextScene(new AsyncOperationHandle<SceneInstance>());
        }
        */

            LoadInNextScene();
        }

        private void LoadInNextScene(AsyncOperationHandle<SceneInstance> loadedOutScene)
        {
            // Debug.Log("Load In Next Scene");
            //currentSceneOperation = Addressables.LoadSceneAsync(sceneToLoadNext, LoadSceneMode.Additive, true);

            StartLoadingStats();

        
            //.Completed += LoadOutLoadingScreen;
        }

        private void LoadInNextScene()
        {
            operation = SceneManager.LoadSceneAsync(sceneToLoadNext, LoadSceneMode.Additive);
            StartLoadingStats();
            //SceneManager.scenelo
        }

        private void LoadOutLoadingScreen()//AsyncOperationHandle<SceneInstance> loadedInScene)
        {
            // Debug.Log("Load Out Loading Screen");
            addressableSceneOpen = true;
            // currentSceneOperation = loadedInScene;
            currentScene = sceneToLoadNext;
            currentOpenScenes.Add(currentScene);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));
            SceneManager.UnloadSceneAsync(loadingScene.SceneName).completed += Finished;
            //Addressables.UnloadSceneAsync(loadingSceneOperation).Completed += Finished;
        }

        private void Finished(AsyncOperation obj)
        {
            showLoadingScreen.Value = false;
            isCurrentlyLoadingScene.Value = false;
        }


        void StartLoadingStats()
        {
            if(loadingStats != null)
            {
                CoroutineMonobehaviour.instance.StopCoroutine(loadingStats);
            }

            loadingStats = LoadingStats();

            CoroutineMonobehaviour.instance.StartCoroutine(loadingStats);
        }

        IEnumerator loadingStats;
        IEnumerator LoadingStats()
        {
            while (true)
            {
                percentAmount.Value = operation.progress;
            
                if(operation.isDone)
                {
                
                    break;
                }

                yield return null;
            }

        

            yield return new WaitForSecondsRealtime(endOfLoadingScreenDelay);
            LoadOutLoadingScreen();
        }
    }
}

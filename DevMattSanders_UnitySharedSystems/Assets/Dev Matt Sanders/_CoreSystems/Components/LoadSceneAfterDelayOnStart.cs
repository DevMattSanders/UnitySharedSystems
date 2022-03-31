using UnityEngine;

namespace DevMattSanders._CoreSystems
{
    public class LoadSceneAfterDelayOnStart : MonoBehaviour
    {
        public float delay = 2;

        public SceneReference sceneToLoad;

        private void Start()
        {
            this.CallWithDelay(LoadScene, delay);
        }


        private void LoadScene()
        {
            SceneLoadManager.instance.LoadScene(sceneToLoad);
        }
    }
}

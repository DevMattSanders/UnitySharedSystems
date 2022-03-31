using Sirenix.OdinInspector;
using UnityEngine;

namespace DevMattSanders._CoreSystems
{
    public class Co_LoadSceneFromReference : MonoBehaviour
    {
        public SceneReference sceneReference;

        [Button]
        public void LoadScene()
        {
            SceneLoadManager.instance.LoadScene(sceneReference);
        }
    }
}

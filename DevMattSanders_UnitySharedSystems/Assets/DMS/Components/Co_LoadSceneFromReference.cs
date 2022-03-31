using DMS.SceneManagement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DMS.Components
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

using UnityEngine;
using UnityEngine.SceneManagement;

namespace DevMattSanders._CoreSystems
{
    public class CurrentOpenScenes : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI textMesh;

        private void Update()
        {
            textMesh.SetText(SceneManager.sceneCount.ToString());
        }
    }
}

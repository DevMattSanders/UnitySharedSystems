using UnityEngine;
using UnityEngine.SceneManagement;

namespace DMS.Components
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

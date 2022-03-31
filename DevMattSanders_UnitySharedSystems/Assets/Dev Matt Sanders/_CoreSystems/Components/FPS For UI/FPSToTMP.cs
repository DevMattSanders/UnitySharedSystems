using TMPro;
using UnityEngine;

namespace DevMattSanders._CoreSystems.FPS_For_UI
{
    public class FPSToTMP : MonoBehaviour
    {
        public TextMeshProUGUI textMesh;
        float deltaTime;
        private void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            textMesh.SetText(Mathf.Ceil(fps).ToString());
        }
    }
}

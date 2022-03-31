using TMPro;
using UnityEngine;

namespace DMS.Components
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

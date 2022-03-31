using UnityEngine;

namespace DevMattSanders._CoreSystems
{
    public class SetActiveOnPlay : MonoBehaviour
    {
        public GameObject targetGameObject;
        public bool setAs;
    

        private void Awake()
        {
            targetGameObject.SetActive(setAs);
        }

    }
}



using UnityEngine;

namespace DMS.Components
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



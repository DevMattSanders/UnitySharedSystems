using DMS.Scriptables;
using UnityEngine;

namespace DMS.Components
{
    public class CreateGameObjectIfNullOnStart : MonoBehaviour
    {
        public GameObjectVariable objectInstance;
        public GameObject objectPrefab;

        public Transform spawnPoint;
        public bool markAsDDOL;

        public bool resetPositionIfAlreadyExists;

        private void Start()
        {        
            if (objectInstance.Value == null && objectInstance.goCounter == 0)
            {
                CreateNewObject();
            }else if (resetPositionIfAlreadyExists)
            {
                SetObjectInstancePosition();
            }
        }

        private void CreateNewObject()
        {
            GameObject go = GameObject.Instantiate(objectPrefab);

            if (markAsDDOL)
            {
                GameObject.DontDestroyOnLoad(go);
            }

            go.AddComponent<InjectedSetAsGameObjectVariable>().Inject(objectInstance);
            SetObjectInstancePosition();
        }

        private void SetObjectInstancePosition()
        {
            if (spawnPoint)
            {
                objectInstance.Value.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                objectInstance.Value.transform.SetPositionAndRotation(transform.position, transform.rotation);
            }
        }
    }
}

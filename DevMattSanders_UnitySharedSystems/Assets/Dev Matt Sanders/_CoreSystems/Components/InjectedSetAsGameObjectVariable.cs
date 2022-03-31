using UnityEngine;

namespace DevMattSanders._CoreSystems
{
    public class InjectedSetAsGameObjectVariable : MonoBehaviour
    {
        public GameObjectVariable injectedVariable;

        private bool injected = false;
        public void Inject(GameObjectVariable goVariable)
        {
            injectedVariable = goVariable;
            goVariable.AddRef(gameObject);

            //goVariable.Value = gameObject;
        
            injected = true;
        }

        private void OnDestroy()
        {
            if (injected)
            {
                if (injectedVariable.Value == gameObject)
                {
                    //Remove self
                    injectedVariable.RemoveRef(gameObject);
                }
            }
        }

    }
}

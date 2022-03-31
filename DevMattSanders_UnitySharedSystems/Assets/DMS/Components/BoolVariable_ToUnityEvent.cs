using DMS.Scriptables;
using UnityEngine;
using UnityEngine.Events;

namespace DMS.Components
{
    public class BoolVariable_ToUnityEvent : MonoBehaviour
    {
        public BoolVariable boolVar;

        public UnityEvent onTrue = new UnityEvent();
        public UnityEvent onFalse = new UnityEvent();
        private void OnEnable()
        {
            boolVar.onValueChanged += Check;
            Check(boolVar.Value);
        }

        private void OnDisable()
        {
            boolVar.onValueChanged -= Check;
        }

        private void Check(bool val)
        {
            if (val)
            {
                onTrue.Invoke();
            }
            else
            {
                onFalse.Invoke();
            }
        }

    
    }
}

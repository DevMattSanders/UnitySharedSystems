using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace DMS.Scriptables
{
    [CreateAssetMenu(menuName = "MattSanders/Events/VoidEvent")]
    [InlineEditor]
    public class VoidEvent : GlobalScriptable
    {
        [Button]
        public void Raise()
        {
            Event.Invoke();
        }

        public override void SoSetStartingValue()
        {
            base.SoSetStartingValue();
            Event.RemoveAllListeners();
        }

        public void Register(UnityAction method)
        {
            Event.AddListener(method);
        }

        public void Unregister(UnityAction method)
        {
            Event.RemoveListener(method);
        }

        [SerializeField] private UnityEvent Event = new UnityEvent();
    }
}

using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace DevMattSanders._CoreSystems
{
    [ExecuteInEditMode]
    public class State : ScriptableObject
    {
        [HideInInspector] public HSM hsm;
        public State parentState;

        public bool stateActiveAsPrimary
        {
            get { return m_stateActiveAsPrimary; }
            set { m_stateActiveAsPrimary = value; onStatePrimaryChanged.Invoke(value); }
        }

        private bool m_stateActiveAsPrimary;

        [HideInInlineEditors] public UnityEvent<bool> onStatePrimaryChanged;

        public bool stateActiveAsSecondary
        {
            get { return m_stateActiveAsSecondary; }
            set { m_stateActiveAsSecondary = value; onStateSecondaryChanged.Invoke(value); }
        }

        private bool m_stateActiveAsSecondary;

        [HideInInlineEditors] public UnityEvent<bool> onStateSecondaryChanged;

        public bool stateActive
        {
            get { return m_stateActive; }
            set { 
                m_stateActive = value;
                if (onStateChanged != null)
                {
                    onStateChanged.Invoke(value);
                }
            }
        }

        [ReadOnly]
        [HideInInlineEditors]
        [SerializeField] private bool m_stateActive;

        [HideInInlineEditors] public UnityEvent<bool> onStateChanged = new UnityEvent<bool>();

        //  [HideInInlineEditors] public List<BoolVariable> keepLockedIfActive = new List<BoolVariable>();
        // [HideInInlineEditors] public List<BoolVariable> keepLockedIfNotActive = new List<BoolVariable>();
        // [HideInInlineEditors] public List<MonoBehaviour> keepStateLockedIsAny = new List<MonoBehaviour>();

   

        //  [HideInInlineEditors] [SerializeField] public StateMono stateMono;

        [HideInInlineEditors] [HideInInspector] public bool currentMainState;

        public void OnDisable()
        {
            stateActiveAsSecondary = false;
        }
        /*
    public void AddToLockedList(MonoBehaviour input)
    {
        if (!keepStateLockedIsAny.Contains(input))
        {
            keepStateLockedIsAny.Add(input);
        }
    }

    public void RemoveFromLockedList(MonoBehaviour input)
    {
        if (keepStateLockedIsAny.Contains(input))
        {
            keepStateLockedIsAny.Remove(input);
        }
    }
    */

        /*
    public bool CanPerformStateToState()
    {
        if (keepStateLockedIsAny.Count == 0)
        {
            return true;
        }
        else
        {
            foreach (object next in keepStateLockedIsAny)
            {
                if (next != null)
                {
                    return false;
                }
            }
        }

        return true;
    }
    */

        [Button("@name"), GUIColor("GetColour")]
        public void EnterState()
        {
            if (hsm) hsm.ChangeState(this);

            /*
        if (stateMono == null)
        {
            Debug.Log("NO STATEMONO ASSIGNED " + this.name);
            return;
        }
        stateMono.hierarchyStateMachine.ChangeState(stateMono);
        */
        }

        public void ExitState()
        {
            if(hsm == null)
            {
                Debug.Log("No HSM");
                return;
            }

            if (stateActive == true)
            {
                if(parentState != null)
                {
                    parentState.EnterState();
                }
                else
                {
                    hsm.SetNoState(); 
                }
                /*
            if (stateMono.parentState != null)
            {
                stateMono.parentState.state.EnterState();
            }
            else
            {
                stateMono.hierarchyStateMachine.SetNoState();
            }
            */
            }

            /*
        if (stateActive == true)
        {
            if (stateMono.parentState != null)
            {
                stateMono.parentState.state.EnterState();
            }
            else
            {
                stateMono.hierarchyStateMachine.SetNoState();
            }
        }
        */
        }

        private Color GetColour()
        {
            if(hsm == null)
            {
                return Color.red;
            }

            if (stateActive)
            {
                return Color.green;
            }
            else
            {
                return Color.grey;
            }
        }
    }

    public enum ActiveState
    {
        Active,
        NotActive
    }

    public enum BoolState
    {
        True,
        False
    }
}
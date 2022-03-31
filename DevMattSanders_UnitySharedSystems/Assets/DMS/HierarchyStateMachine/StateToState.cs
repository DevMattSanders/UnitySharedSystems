using DMS.Extensions;
using UnityEngine;

namespace DMS.HierarchyStateMachine
{
    public class StateToState : MonoBehaviour
    {
        //public bool onlyIfMainState;
        public float delay;

        private StateMono parentState;

        public StateMono targetState;

        private void Awake()
        {
            parentState = GetComponent<StateMono>();

            if (parentState != null)
            {
                //  Debug.Log("HERE");
                //    parentState.enteredState += Begin;
                if (parentState.state)
                {
                    parentState.state.onStateChanged.AddListener(StateChanged);
                }
            }
        }

        private void OnDestroy()
        {
            if (parentState)
            {
                if (parentState.state)
                {
                    parentState.state.onStateChanged.RemoveListener(StateChanged);
                }
            }
        }

        private void StateChanged(bool value)
        {
            if (value && parentState != null && targetState != null)
            {
                this.CallWithDelay(EnterState, delay);
            }
        }

        /*        
        public void Begin(StateMono state)
        {
            if (parentState != null && targetState != null)
            {
                this.CallWithDelay(EnterState, delay);
            }
        }
        */

        private void EnterState()
        {/*
            if (parentState.state.stateActiveAsSecondary)
            {
              //  if (parentState.state.CanPerformStateToState() == false)
              //  {
              //      this.CallWithDelay(EnterState, 0.1f);
              //      Debug.Log("State Locked!");
              //      return;
             //   }

                if (onlyIfMainState == false || parentState.state.hsm.currentState == parentState)
                {
                    targetState.state.EnterState();
                }
            }
            */
            if (parentState.state.hsm.currentState == parentState)
            {
                targetState.state.EnterState();
            }
        }
    }
}
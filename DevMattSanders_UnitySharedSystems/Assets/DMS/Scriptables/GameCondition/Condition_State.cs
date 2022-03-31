using System;
using DMS.HierarchyStateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DMS.Scriptables
{
    [System.Serializable]
    [HideReferenceObjectPicker]
    public class Condition_State : GenericCondition
    {
        [HorizontalGroup("V", Width = 80)]
        [HideLabel]
        [GUIColor("$GuiColour")]
        [DisableInPlayMode]
        public ActiveState compareTo = ActiveState.Active;

        [HorizontalGroup("V")]
        [HideLabel]
        [InlineEditor]
        [GUIColor("$CurrentGuiColour")]
        public State stateValue;

        public enum ActiveState
        {
            Active,
            NotActive
        }

        private Color GuiColour()
        {
            if (compareTo == ActiveState.Active)
            {
                return new Color(0.8f, 1, 1f);

            }
            else
            {
                return new Color(1, 0.8f, 1);
            }
        }

        private Color CurrentGuiColour()
        {
            if (stateValue == null)
            {
                return new Color(1, 0.95f, 1);
            }

            if (stateValue.stateActive == true)
            {
                return new Color(0.95f, 1, 1f);
            }
            else
            {
                return new Color(1, 0.95f, 1);
            }
        }

        public override void AwakeInput(Action<bool> onChanged)
        {
            base.AwakeInput(onChanged);
            // stateValue.onStateChanged += StateChanged;
            stateValue.onStateChanged.AddListener(StateChanged);
        }

        public override void DestroyInput()
        {
            base.DestroyInput();
            //stateValue.onStateChanged -= StateChanged;
            stateValue.onStateChanged.RemoveListener(StateChanged);
        }

        private void StateChanged(bool val)
        {
            callOnMetChanged.Invoke(GetConditionMet());
        }

        public override bool GetConditionMet()
        {
            if (stateValue == null) { Debug.Log("STATE NULL!"); return true; }

            if (stateValue.stateActive == true && compareTo == ActiveState.Active
                || stateValue.stateActive == false && compareTo == ActiveState.NotActive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void SetMet(bool setMet)
        {
            if (setMet)
            {
                if (compareTo == ActiveState.Active)
                {
                    stateValue.EnterState();
                }
                else
                {
                    stateValue.ExitState();
                }
            }
            else
            {
                if (compareTo == ActiveState.Active)
                {
                    stateValue.ExitState();
                }
                else
                {
                    stateValue.EnterState();
                }
            }
        }
    }
}
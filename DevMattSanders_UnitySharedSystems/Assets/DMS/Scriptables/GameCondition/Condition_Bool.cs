using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DMS.Scriptables
{
    [System.Serializable]
    public class Condition_Bool : GenericCondition
    {
        [HorizontalGroup("V", Width = 80)]
        [HideLabel]
        [GUIColor("$GuiColour")]
        [DisableInPlayMode]
        public BoolState compareTo = BoolState.True;

        [HorizontalGroup("V")]
        [HideLabel]
        [InlineEditor]
        [GUIColor("$CurrentGuiColour")]
        public BoolVariable boolValue;

        private Color GuiColour()
        {
            if (compareTo == BoolState.True)
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
            if (boolValue == null)
            {
                return new Color(1, 0.95f, 1);
            }

            if (boolValue.Value == true)
            {
                return new Color(0.95f, 1, 1f);
            }
            else
            {
                return new Color(1, 0.95f, 1);
            }
        }

        public enum BoolState
        {
            True,
            False
        }

        public override void AwakeInput(Action<bool> onChanged)
        {
            base.AwakeInput(onChanged);

            boolValue.onValueChanged += BoolChanged;
        }


        public override void DestroyInput()
        {
            base.DestroyInput();
            boolValue.onValueChanged -= BoolChanged;
        }

        private void BoolChanged(bool val)
        {
            callOnMetChanged.Invoke(GetConditionMet());
        }

        public override bool GetConditionMet()
        {
            if (boolValue == null) { Debug.Log("BOOL NULL!"); return true; }

            if (boolValue.Value == true && compareTo == BoolState.True
                || boolValue.Value == false && compareTo == BoolState.False)
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
                if (compareTo == BoolState.True)
                {
                    boolValue.Value = true;
                }
                else
                {
                    boolValue.Value = false;
                }
            }
            else
            {
                if (compareTo == BoolState.True)
                {
                    boolValue.Value = false;
                }
                else
                {
                    boolValue.Value = true;
                }
            }
        }


    }
}
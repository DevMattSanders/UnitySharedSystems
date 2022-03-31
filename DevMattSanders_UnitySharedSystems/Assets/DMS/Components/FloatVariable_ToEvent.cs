using System.Collections;
using DMS.Scriptables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace DMS.Components
{
    public class FloatVariable_ToEvent : MonoBehaviour
    {
        public FloatVariable floatVariable;
        public UnityEvent<float> floatEvent = new UnityEvent<float>();
    


        private void Start()
        {
            floatVariable.onValueChanged += FloatChanged;
            FloatChanged(floatVariable.Value);
        }

        private void OnDestroy()
        {
            floatVariable.onValueChanged -= FloatChanged;
        }

        public void FloatChanged(float importedValue)
        {
            if (animateValue)
            {
                if (resetToZeroIfNewValueIsLowerThanCurrent)
                {
                    if (importedValue < value) { value = 0; floatEvent.Invoke(importedValue); }
                }

                SetAnimatedValue(importedValue);
            }
            else
            {
                floatEvent.Invoke(importedValue);
            }
        
        }
        public bool animateValue = false;
        [ShowIf("animateValue"), Min(0.05f)] public float animatedRecoveryRate = 1;
        [ShowIf("animateValue"),SerializeField,ReadOnly,HorizontalGroup("H")] private float value;
        [ShowIf("animateValue"),SerializeField,ReadOnly, HorizontalGroup("H")] private float valueTarget;

        //[ShowIf("animateValue")]public bool resetToZeroOnTargetOnSecondAnimatedLoad = false;
        [ShowIf("animateValue")] public bool resetToZeroIfNewValueIsLowerThanCurrent = false;


        void SetAnimatedValue(float _valueTarget)
        {
            //  if (resetToZeroOnTargetOnSecondAnimatedLoad)
            // {
            //     if (value == valueTarget) value = 0;
            // }

            valueTarget = _valueTarget;

            if (animatedValue != null)
            {
                CoroutineMonobehaviour.instance.StopCoroutine(animatedValue);
            }

            animatedValue = AnimatedValue();

            CoroutineMonobehaviour.instance.StartCoroutine(animatedValue);
        }

   



        IEnumerator animatedValue;
        IEnumerator AnimatedValue()
        {
            while (value != valueTarget)
            {
                value = Mathf.MoveTowards(value, valueTarget, animatedRecoveryRate * Time.deltaTime);
                floatEvent.Invoke(value);

                yield return null;
            }

            floatEvent.Invoke(value);
        }


    }
}

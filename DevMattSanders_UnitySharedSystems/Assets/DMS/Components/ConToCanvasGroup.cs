using DG.Tweening;
using DMS.Scriptables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DMS.Components
{
    public class ConToCanvasGroup : MonoBehaviour
    {
        public BoolReference boolRef;

        public CanvasGroup canvasGroup;

        public bool useStartingValue;
        [PropertyRange(0,1),ShowIf("useStartingValue")]
        public float startingValue;

        public GameCondition condition;
        private Tween tween;

        public bool conMetEqualsAlphaOne = true;

        public float delay;
        public float duration = 0.5f;
        public Ease ease = Ease.InOutSine;

        private void Awake()
        {
            if (useStartingValue)
            {
                canvasGroup.alpha = startingValue;
            }
        }

        private void OnEnable()
        {
            condition.AddListener(ConChanged);
            ConChanged(condition.conditionMet);
        }

        private void OnDisable()
        {
            condition.RemoveListener(ConChanged);
        }

 

        private void ConChanged(bool val)
        {
            if (tween != null) tween.Kill();

            if (conMetEqualsAlphaOne)
            {
                if (val)
                {
                    tween = canvasGroup.DOFade(1, duration).SetDelay(delay).SetEase(ease);
                }
                else
                {
                    tween = canvasGroup.DOFade(0, duration).SetDelay(delay).SetEase(ease);
                }
            }
            else
            {
                if (val)
                {
                    tween = canvasGroup.DOFade(0, duration).SetDelay(delay).SetEase(ease);
                }
                else
                {
                    tween = canvasGroup.DOFade(1, duration).SetDelay(delay).SetEase(ease);
                }
            }
        }
    }
}

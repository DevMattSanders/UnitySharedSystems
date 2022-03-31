using DG.Tweening;
using UnityEngine;

namespace DevMattSanders._CoreSystems
{
    public class BoolVariable_ToCanvasGroupAlpha : MonoBehaviour
    {
        public BoolVariable boolVar;
        public CanvasGroup canvasGroup;

        public bool trueEqualsTransparent;
        public float fadeSpeed = 0.5f;
        private void OnEnable()
        {
            boolVar.onValueChanged += Check;
            Check(boolVar.Value);
        }

        private void OnDisable()
        {
            boolVar.onValueChanged -= Check;
        }
        private Tween fadeTween;

        private void Check(bool val)
        {
            if (fadeTween != null) fadeTween.Kill();
            if (trueEqualsTransparent) {
                if (val)
                {
                    fadeTween = canvasGroup.DOFade(0,fadeSpeed).SetEase(Ease.InOutSine);
                }
                else
                {
                    fadeTween = canvasGroup.DOFade(1, fadeSpeed).SetEase(Ease.InOutSine);
                }
            }
            else
            {
                if (val)
                {
                    fadeTween = canvasGroup.DOFade(1, fadeSpeed).SetEase(Ease.InOutSine);
                }
                else
                {
                    fadeTween = canvasGroup.DOFade(0, fadeSpeed).SetEase(Ease.InOutSine);
                }
            }
        }

    
    }
}

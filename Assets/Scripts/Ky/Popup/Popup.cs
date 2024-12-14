using System.Collections;
using TMPro;
using UnityEngine;

namespace Ky.Popup
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private float offsetDistance = 1f;
        [SerializeField] private float duration = 0.9f;
        [SerializeField] private AnimationCurve scaleCurve;
        [Tooltip("Should start from 0")] [SerializeField] private AnimationCurve positionCurve;
        [SerializeField] private AnimationCurve randomizedXOffsetCurve;
        [SerializeField] private float yOffset = 0.2f;
        [SerializeField] private Vector2 randomizerRange = new Vector2(1f, 1.6f);

        private Gradient colorGradient;
        private RectTransform rectTransform;

        public void Initialize(Vector3 startingPosition, PopupDep dep)
        {
            textMeshProUGUI.text = dep.text;
            colorGradient = dep.colorGradient;
            rectTransform = (RectTransform)transform;

            SetPosition(startingPosition);
            StartCoroutine(PopupCoroutine());
        }

        private void SetPosition(Vector3 startingPosition)
        {
            RectTransform rectTr = (RectTransform)transform;
            if (Camera.main != null)
                rectTr.position = startingPosition - offsetDistance * Camera.main.transform.forward;
            rectTr.sizeDelta = Vector2.one;
        }

        private IEnumerator PopupCoroutine()
        {
            float time = 0;
            Vector3 startPos = rectTransform.localPosition + Vector3.up * yOffset;
            float xOffset = Random.Range(-1f, 1f);
            float yRandomizer = Random.Range(randomizerRange.x, randomizerRange.y);
            while (time < duration)
            {
                time += Time.deltaTime;
                float ratio = time / duration;
                float scale = scaleCurve.Evaluate(ratio);
                var x = Vector3.right * (randomizedXOffsetCurve.Evaluate(ratio) * xOffset);
                var y = Vector3.up * (positionCurve.Evaluate(ratio) * yRandomizer);
                rectTransform.localScale = Vector3.one * scale;
                rectTransform.localPosition = startPos + y + x;
                textMeshProUGUI.color = colorGradient.Evaluate(ratio);

                float endRatioThreshold = 0.85f;
                if (ratio > endRatioThreshold)
                {
                    float endRatio = (ratio - endRatioThreshold) / (1 - endRatioThreshold);
                    Color c = textMeshProUGUI.color;
                    c.a = 1 - endRatio;
                    textMeshProUGUI.color = c;
                }

                yield return null;
            }

            Destroy(gameObject);
        }
    }

    public class PopupDep
    {
        public string text;
        public Gradient colorGradient;
    }
}
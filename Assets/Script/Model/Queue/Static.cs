using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.UI
{
    public sealed class Static : MonoBehaviour
    {
        [Header("Flicker effect")]
        [SerializeField]
        private float minInterval = .1f;
        [SerializeField]
        private float maxInterval = .75f;
        [SerializeField]
        private float minDuration = .1f;
        [SerializeField]
        private float maxDuration = .15f;
        private List<Image> fritz = new();
        private List<Coroutine> fritzes = new();

        private void Awake()
        {
            fritz = gameObject.GetComponentsInChildren<Image>().ToList();
            foreach (Image image in fritz)
            {
                fritzes.Add(StartCoroutine(Flicker(image)));
            }
        }

        public void Stop()
        {
            foreach (Coroutine flicker in fritzes)
            {
                StopCoroutine(flicker);
            }
        }

        private IEnumerator Flicker(Image image)
        {
            while (true)
            {
                // Cooldown
                float interval = UnityEngine.Random.Range(minInterval, maxInterval);
                yield return new WaitForSeconds(interval);

                // ToggleFadeInOut out
                float duration = UnityEngine.Random.Range(minDuration, maxDuration);
                StartCoroutine(image.ToggleFadeInOut(duration));
                yield return new WaitForSeconds(duration);

                // ToggleFadeInOut back in
                StartCoroutine(image.ToggleFadeInOut(duration));
                yield return new WaitForSeconds(duration);
            }
        }
    }
}

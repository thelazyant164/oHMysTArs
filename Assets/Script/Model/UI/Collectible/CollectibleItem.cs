using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.Collectible
{
    public sealed class CollectibleItem : MonoBehaviour
    {
        [Header("Flash effect")]
        [SerializeField]
        private float lockedOpacity = .1f;
        [SerializeField]
        private float flashMinOpacity = .5f;
        [SerializeField]
        private float flashMaxOpacity = 1f;
        [SerializeField]
        private float flashInterval = .2f;
        [SerializeField]
        private float flashDuration = .1f;
        [Space]

        [Header("Element")]
        [SerializeField]
        private Image frame;
        [SerializeField]
        private Image item;
        [SerializeField]
        private Image locked;

        public void InitUnlock(Texture2D unlockTexture)
        {
            item.SetTexture(unlockTexture);
            locked.gameObject.SetActive(false);
            StartCoroutine(frame.Flash(flashMaxOpacity, flashMinOpacity, flashInterval, flashDuration));
        }

        public void InitLock(Texture2D lockTexture)
        {
            item.SetTexture(lockTexture);
            locked.gameObject.SetActive(true);
            StartCoroutine(frame.FadeTo(lockedOpacity, 1f));
            StartCoroutine(locked.FadeTo(lockedOpacity, 1f));
            StartCoroutine(item.Flash(flashMaxOpacity, flashMinOpacity, flashInterval, flashDuration));
        }
    }
}

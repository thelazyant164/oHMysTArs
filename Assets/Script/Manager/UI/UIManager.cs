using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.UI
{
    public sealed class UIManager : Singleton<UIManager>
    {
        public Timeline Timeline { get; private set; }
        public AudioSource UIAudio { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            Timeline = GetComponentInChildren<Timeline>();
            UIAudio = GetComponentInChildren<AudioSource>();
        }
    }
}

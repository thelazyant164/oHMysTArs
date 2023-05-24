using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.UI
{
    public sealed class UIManager : Singleton<UIManager>
    {
        public AudioSource UIAudio { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            UIAudio = GetComponentInChildren<AudioSource>();
        }
    }
}

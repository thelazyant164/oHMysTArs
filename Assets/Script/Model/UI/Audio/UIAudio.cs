using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.UI
{
    public sealed class UIAudio : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<AudioSource>().ignoreListenerPause = true;
        }
    }
}

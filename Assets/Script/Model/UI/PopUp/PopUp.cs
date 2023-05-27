using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Diagnostics.Tracing;

namespace Com.oHMysTArs.UI
{
    public abstract class PopUp : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            Reset();
        }

        protected virtual void Reset() { }
    }
}

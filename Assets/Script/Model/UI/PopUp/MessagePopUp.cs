using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Diagnostics.Tracing;
using UnityEngine.UI;

namespace Com.oHMysTArs.UI
{
    public class MessagePopUp : PopUp
    {
        [SerializeField]
        private TextMeshProUGUI message;

        [SerializeField]
        private Button dismissButton;

        private void Awake()
        {
            dismissButton.onClick.AddListener(OnDismiss);
        }

        public void Show(string message)
        {
            this.message.text = message;
            base.Show();
        }

        protected override void Reset()
        {
            message.SetText("");
        }

        private void OnDismiss() => Hide();
    }
}

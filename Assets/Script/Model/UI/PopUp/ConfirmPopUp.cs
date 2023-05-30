using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Diagnostics.Tracing;
using UnityEngine.UI;

namespace Com.oHMysTArs.UI
{
    public sealed class ConfirmPopUp : PopUp
    {
        [SerializeField]
        private Button cancelButton;
        [SerializeField]
        private Button proceedButton;

        [SerializeField]
        private TextMeshProUGUI warning;

        private void Awake()
        {
            cancelButton.onClick.AddListener(() => Hide());
        }

        public void Show(
            string warning,
            Action onProceedCallback
        )
        {
            this.warning.SetText(warning);
            proceedButton.onClick.AddListener(() => onProceedCallback());
            base.Show();
        }
    }
}

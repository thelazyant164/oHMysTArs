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
        public struct ConfirmPopUpContent
        {
            public string title;
            public string description;
            public string confirmBtnText;
            public string dismissBtnText;

            private static ConfirmPopUpContent DefaultConfirmPopUpContent()
            {
                return new ConfirmPopUpContent()
                {
                    title = "",
                    description = "",
                    confirmBtnText = "Confirm",
                    dismissBtnText = "Dismiss"
                };
            }

            public ConfirmPopUpContent(string title, string description)
            {
                this = DefaultConfirmPopUpContent();
                this.title = title;
                this.description = description;
            }

            public ConfirmPopUpContent(
                string title,
                string description,
                string confirmBtnText,
                string dismissBtnText
            )
            {
                this.title = title;
                this.description = description;
                this.confirmBtnText = confirmBtnText;
                this.dismissBtnText = dismissBtnText;
            }
        }

        [SerializeField]
        private Button dismissButton;

        [SerializeField]
        private Button proceedButton;

        [SerializeField]
        private TextMeshProUGUI title;

        [SerializeField]
        private TextMeshProUGUI description;
        private Action _proceedDelegate;
        private Action _dismissDelegate;

        private void Awake()
        {
            proceedButton.onClick.AddListener(OnProceed);
            dismissButton.onClick.AddListener(OnDismiss);
        }

        public void Show(
            ConfirmPopUpContent content,
            Action onProceedCallback,
            Action onDismissCallback
        )
        {
            if (content.title?.Length > 0)
            {
                title.gameObject.SetActive(true);
                title.SetText(content.title);
            }
            if (content.description?.Length > 0)
            {
                description.gameObject.SetActive(true);
                description.SetText(content.description);
            }
            _proceedDelegate = onProceedCallback;
            _dismissDelegate = onDismissCallback;
            base.Show();
        }

        protected override void Reset()
        {
            title.SetText("");
            title.gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText("");
            description.SetText("");
            title.gameObject.SetActive(false);
            description.gameObject.SetActive(false);
        }

        private void OnProceed()
        {
            if (_proceedDelegate != null)
            {
                _proceedDelegate();
            }
            Hide();
        }

        private void OnDismiss()
        {
            if (_dismissDelegate != null)
            {
                _dismissDelegate();
            }
            Hide();
        }
    }
}

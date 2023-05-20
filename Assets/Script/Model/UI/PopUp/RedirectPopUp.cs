using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Diagnostics.Tracing;
using UnityEngine.UI;

namespace Com.oHMysTArs.UI
{
    public class RedirectPopUp : PopUp
    {
        public struct RedirectPopUpContent
        {
            public string title;
            public string description;
            public string confirmBtnText;

            private static RedirectPopUpContent DefaultRedirectPopUpContent()
            {
                return new RedirectPopUpContent()
                {
                    title = "",
                    description = "",
                    confirmBtnText = "Confirm",
                };
            }

            public RedirectPopUpContent(string title, string description)
            {
                this = DefaultRedirectPopUpContent();
                this.title = title;
                this.description = description;
            }

            public RedirectPopUpContent(string title, string description, string confirmBtnText)
            {
                this.title = title;
                this.description = description;
                this.confirmBtnText = confirmBtnText;
            }
        }

        [SerializeField]
        private Button proceedButton;

        [SerializeField]
        private TextMeshProUGUI title;

        [SerializeField]
        private TextMeshProUGUI description;
        private Action _proceedDelegate;

        private void Awake()
        {
            proceedButton.onClick.AddListener(OnProceed);
        }

        public void Show(RedirectPopUpContent content, Action onProceedCallback)
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
    }
}

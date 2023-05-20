using Com.oHMysTArs.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.UI
{
    public sealed class AssessmentPopUp : PopUp
    {
        [SerializeField]
        private Button backButton;
        [SerializeField]
        private Button replayButton;
        [SerializeField]
        private Button proceedButton;

        [SerializeField]
        private TextMeshProUGUI title;
        [SerializeField]
        private StarRating overallRating;
        [SerializeField]
        private TextMeshProUGUI total;
        [SerializeField]
        private TextMeshProUGUI succeed;

        [SerializeField]
        private StarRating accuracyRating;
        [SerializeField]
        private StarRating punctualityRating;
        [SerializeField]
        private StarRating profitabilityRating;

        private Action _backDelegate;
        private Action _replayDelegate;
        private Action _proceedDelegate;

        private void Awake()
        {
            backButton.onClick.AddListener(OnBack);
            replayButton.onClick.AddListener(OnReplay);
            proceedButton.onClick.AddListener(OnProceed);
        }

        public void Show(
            LevelAssessment content,
            Action onBackCallback,
            Action onReplayCallback,
            Action onProceedCallback
        )
        {
            title.gameObject.SetActive(true);
            title.SetText(content.Name);

            // TODO: finish setup report scene
            overallRating.Init(content.OverallRating);
            accuracyRating.Init(content.Accuracy);
            punctualityRating.Init(content.Punctuality);
            profitabilityRating.Init(content.Profitability);

            _backDelegate = onBackCallback;
            _replayDelegate = onReplayCallback;
            _proceedDelegate = onProceedCallback;
            base.Show();
        }

        protected override void Reset()
        {
            //title.SetText("");
            //title.gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText("");
            //title.gameObject.SetActive(false);
            overallRating.Reset();
            accuracyRating.Reset();
            punctualityRating.Reset();
            profitabilityRating.Reset();
        }

        private void OnBack()
        {
            if (_backDelegate != null)
            {
                _backDelegate();
            }
            Hide();
        }

        private void OnReplay()
        {
            if (_replayDelegate != null)
            {
                _replayDelegate();
            }
            Hide();
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

using Com.oHMysTArs.Assessment;
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
        [Header("Feedback")]
        [SerializeField]
        private GameObject feedbackPrefab;
        [SerializeField]
        private GameObject feedbackForum;
        [Space]

        [Header("Content")]
        [SerializeField]
        private TextMeshProUGUI title;
        [SerializeField]
        private StarRating overallRating;
        [SerializeField]
        private TextMeshProUGUI total;
        [SerializeField]
        private TextMeshProUGUI succeed;
        [Space]

        [Header("Control")]
        [SerializeField]
        private Button backButton;
        [SerializeField]
        private Button replayButton;
        [SerializeField]
        private Button proceedButton;

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

            _backDelegate = onBackCallback;
            _replayDelegate = onReplayCallback;
            _proceedDelegate = onProceedCallback;
            base.Show();
        }

        protected override void Reset()
        {
            overallRating.Reset();
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

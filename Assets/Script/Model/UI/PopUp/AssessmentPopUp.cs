using Com.oHMysTArs.Assessment;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Com.oHMysTArs.Level;

namespace Com.oHMysTArs.UI
{
    public sealed class AssessmentPopUp : PopUp
    {
        [SerializeField]
        private Transform feedbackForum;
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

        private LevelSelected selected;
        private LevelManager levelManager;

        protected override void Start()
        {
            selected = LevelSelected.Instance;
            levelManager = GameManager.Instance.LevelManager;
            base.Start();
        }

        public void Show(
            LevelAssessment content,
            FeedbackSO[] feedbacks
        )
        {
            title.gameObject.SetActive(true);
            title.SetText(content.Name);
            overallRating.Init(content.OverallRating);
            foreach (FeedbackSO feedback in feedbacks)
            {
                GameManager.Instance.FeedbackManager.CreateFeedback(feedback, feedbackForum);
            }
            total.SetText(content.Total.ToString());
            succeed.SetText(content.Succeed.ToString());

            backButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("MenuScene"));
            replayButton.onClick.AddListener(() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
            proceedButton.onClick.AddListener(() => 
            {
                if (levelManager.TryGetNextLevel(selected.Selected, out string nextLvl))
                {
                    selected.SelectLevel(nextLvl);
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex); 
                }
                else
                {
                    SceneManager.LoadSceneAsync("CreditScene");
                }
            });
            base.Show();
        }

        protected override void Reset()
        {
            overallRating.Reset();
        }
    }
}

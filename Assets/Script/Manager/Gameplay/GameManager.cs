using Com.oHMysTArs.Level;
using Com.oHMysTArs.Assessment;
using Com.oHMysTArs.Pattern;
using Com.oHMysTArs.Spaceship;
using Com.oHMysTArs.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.oHMysTArs
{
    public sealed class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private string levelName;
        public DrawHistory DrawHistory { get; private set; }
        public LevelManager LevelManager { get; private set; }
        public FeedbackManager FeedbackManager { get; private set; }
        public SpaceshipManager SpaceshipManager { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            DrawHistory = GetComponentInChildren<DrawHistory>();
            LevelManager = GetComponentInChildren<LevelManager>();
            FeedbackManager = GetComponentInChildren<FeedbackManager>();
            SpaceshipManager = GetComponentInChildren<SpaceshipManager>();
        }

        private void Start()
        {
            SpaceshipManager.Init();
            DrawHistory.Init();
            UIManager.Instance.Timeline.Init();
            LevelManager.PlayLevel(levelName);
            LevelManager.OnFinish += ShowLevelAssessment;
        }

        private void ShowLevelAssessment(object sender, Level.Level level)
        {
            LevelAssessment assessment = new LevelAssessment(level.name, SpaceshipManager.Done);
            FeedbackSO[] feedbacks = FeedbackManager.CreateFeedbacks(assessment);
            PopUpManager.Instance.ShowAssessment(assessment, feedbacks, 
                () => SceneManager.LoadSceneAsync("Menu"), 
                () => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex),
                // TODO: as there is only 1 level right now, "proceed" replays level - make levels progress in order
                () => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
        }
    }
}

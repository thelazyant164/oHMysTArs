using Com.oHMysTArs.Level;
using Com.oHMysTArs.Assessment;
using Com.oHMysTArs.Pattern;
using Com.oHMysTArs.Spaceship;
using Com.oHMysTArs.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.oHMysTArs.Tutorial;
using Com.oHMysTArs.Collectible;

namespace Com.oHMysTArs
{
    public sealed class GameManager : Singleton<GameManager>
    {
        private LevelSelected levelSelected;
        [SerializeField]
        private Queue.Queue queue;
        public DrawHistory DrawHistory { get; private set; }
        public LevelManager LevelManager { get; private set; }
        public FeedbackManager FeedbackManager { get; private set; }
        public TutorialManager TutorialManager { get; private set; }
        public bool InTutorialMode => TutorialManager != null && TutorialManager.isActiveAndEnabled;
        public SpaceshipManager SpaceshipManager { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            levelSelected = LevelSelected.Instance;
            DrawHistory = GetComponentInChildren<DrawHistory>();
            LevelManager = GetComponentInChildren<LevelManager>();
            FeedbackManager = GetComponentInChildren<FeedbackManager>();
            TutorialManager = levelSelected.Selected == "1" ? GetComponentInChildren<TutorialManager>() : null;
            SpaceshipManager = GetComponentInChildren<SpaceshipManager>();
        }

        private void Start()
        {
            SpaceshipManager.Init();
            DrawHistory.Init();
            UIManager.Instance.Timeline.Init();
            queue.Init();
            LevelManager.PlayLevel(levelSelected.Selected);
            LevelManager.OnFinish += ShowLevelAssessment;
        }

        public void TogglePause(bool pause)
        {
            if (pause)
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
            else
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
            }
        }

        private void ShowLevelAssessment(object sender, Level.Level level)
        {
            LevelAssessment assessment = new LevelAssessment(level.name, SpaceshipManager.Done);
            new LevelResult(assessment.Name, assessment.OverallRating).Save();
            foreach(Spaceship.Spaceship spaceship in SpaceshipManager.Done)
            {
                new SpaceshipCollectible(spaceship.Name).Save();
            }
            FeedbackSO[] feedbacks = FeedbackManager.CreateFeedbacks(assessment);
            PopUpManager.Instance.ShowAssessment(assessment, feedbacks);
        }
    }
}

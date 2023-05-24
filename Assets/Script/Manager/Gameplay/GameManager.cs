using Com.oHMysTArs.Level;
using Com.oHMysTArs.Pattern;
using Com.oHMysTArs.Spaceship;
using Com.oHMysTArs.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs
{
    public sealed class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private string levelName;
        public DrawHistory DrawHistory { get; private set; }
        public LevelManager LevelManager { get; private set; }
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
            SpaceshipManager = GetComponentInChildren<SpaceshipManager>();
            SpaceshipManager.Init();
        }

        private void Start()
        {
            DrawHistory.Init();
            LevelManager.PlayLevel(levelName);
            LevelManager.OnFinish += ShowLevelAssessment;
        }

        private void ShowLevelAssessment(object sender, Level.Level level)
        {
            PopUpManager.Instance.ShowAssessment(new LevelAssessment(level.name, SpaceshipManager.Done));
        }
    }
}

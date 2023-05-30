using Com.oHMysTArs.Spaceship;
using Com.oHMysTArs.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Com.oHMysTArs.Level
{
    public sealed class LevelManager : MonoBehaviour
    {
        private Level current;
        private readonly List<Level> levels = new();
        private SpaceshipManager spaceshipManager;
        private Timeline timeline;
        public event EventHandler<Level> OnStart;
        public event EventHandler<Level> OnFinish;
        [SerializeField]
        private AudioClip winSFX;
        [SerializeField]
        private AudioClip loseSFX;

        private void Awake()
        {
            levels.AddRange(Resources.LoadAll<Level>("Level"));
        }

        private void Start()
        {
            timeline = UIManager.Instance.Timeline;
            spaceshipManager = GameManager.Instance.SpaceshipManager;
            spaceshipManager.OnEndQueue += EndLevelWin;
            timeline.OnTimerEnd += EndLevelLose;
        }

        private void EndLevelWin(object sender, EventArgs e)
        {
            UIManager.Instance.UIAudio.PlayOneShot(winSFX);
            OnFinish?.Invoke(this, current);
        }

        private void EndLevelLose(object sender, EventArgs e)
        {
            UIManager.Instance.UIAudio.PlayOneShot(loseSFX);
            OnFinish?.Invoke(this, current);
        }

        public void PlayLevel(string name)
        {
            current = levels.Find(level => level.name == name);
            OnStart?.Invoke(this, current);
        }
    }
}

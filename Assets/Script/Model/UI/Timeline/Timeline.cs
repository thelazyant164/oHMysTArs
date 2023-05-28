using Com.oHMysTArs.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.UI
{
    public sealed class Timeline : MonoBehaviour
    {
        [SerializeField]
        private float timePerSpaceship = 8;
        private Slider timeline;
        private Coroutine timer;
        private LevelManager levelManager;
        public event EventHandler OnTimerEnd;
        [Space]

        [Header("SFX")]
        [SerializeField]
        private AudioClip timerStartSFX;
        [SerializeField]
        private AudioClip countdownSFX;
        private AudioSource audioSource;
        private bool playCountdown = false;

        private void Awake()
        {
            timeline = GetComponentInChildren<Slider>();
            audioSource = GetComponentInChildren<AudioSource>();
        }

        public void Init()
        {
            levelManager = GameManager.Instance.LevelManager;
            levelManager.OnStart += (object sender, Level.Level level) => timer = StartCoroutine(StartTimer(timePerSpaceship * level.Queue.Length));
            levelManager.OnFinish += (object sender, Level.Level level) =>
            {
                audioSource.Stop();
                if (timer != null) StopCoroutine(timer);
            };
        }

        private IEnumerator StartTimer(float time)
        {
            audioSource.PlayOneShot(timerStartSFX);
            timeline.maxValue = time;
            timeline.minValue = 0;
            while (time > 0)
            {
                time -= Time.deltaTime;
                timeline.value = time;
                if (!playCountdown && time <= 10)
                {
                    playCountdown = true;
                    audioSource.PlayOneShot(countdownSFX);
                }
                yield return null;
            }
            timer = null;
            OnTimerEnd?.Invoke(this, EventArgs.Empty);
        }
    }
}

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

        private void Awake()
        {
            timeline = GetComponentInChildren<Slider>();
        }

        public void Init()
        {
            levelManager = GameManager.Instance.LevelManager;
            levelManager.OnStart += (object sender, Level.Level level) => timer = StartCoroutine(StartTimer(timePerSpaceship * level.Queue.Length));
            levelManager.OnFinish += (object sender, Level.Level level) =>
            {
                if (timer != null) StopCoroutine(timer);
            };
        }

        private IEnumerator StartTimer(float time)
        {
            timeline.maxValue = time;
            timeline.minValue = 0;
            while (time > 0)
            {
                time -= Time.deltaTime;
                timeline.value = time;
                yield return null;
            }
            timer = null;
            OnTimerEnd?.Invoke(this, EventArgs.Empty);
        }
    }
}

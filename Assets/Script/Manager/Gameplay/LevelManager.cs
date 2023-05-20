using Com.oHMysTArs.Spaceship;
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
        private List<Level> levels;
        private SpaceshipManager spaceshipManager;
        public event EventHandler<Level> OnStart;
        public event EventHandler<Level> OnFinish;

        private void Awake()
        {
            levels = Resources.LoadAll("Level", typeof(Level)).Cast<Level>().ToList();
        }

        void Start()
        {
            spaceshipManager = GameManager.Instance.SpaceshipManager;
            spaceshipManager.OnEndQueue += EndLevel;
        }

        private void EndLevel(object sender, EventArgs e)
        {
            OnFinish?.Invoke(this, current);
        }

        public void PlayLevel(string name)
        {
            current = levels.Find(level => level.name == name);
            OnStart?.Invoke(this, current);
        }
    }
}

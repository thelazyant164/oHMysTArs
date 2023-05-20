using Com.oHMysTArs.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Com.oHMysTArs.Spaceship
{
    public sealed class SpaceshipManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject spaceshipPrefab;
        [SerializeField]
        private List<Spaceship> waitingQueue;
        private List<Spaceship> done;
        public List<Spaceship> Done => done;
        public Spaceship Current => waitingQueue.FirstOrDefault();
        private LevelManager levelManager;
        public event EventHandler<Spaceship> OnActiveSpaceshipChange;
        public event EventHandler OnEndQueue;

        private void Start()
        {
            levelManager = GameManager.Instance.LevelManager;
            levelManager.OnStart += (object sender, Level.Level level) => InitQueue(level.Queue);
        }

        public void Next()
        {
            Spaceship last = Current;
            if (last != null)
            {
                waitingQueue.RemoveAt(0);
                done.Add(last);
            }
            if (Current != null)
            {
                OnActiveSpaceshipChange?.Invoke(this, Current);
            }
            else
            {
                OnEndQueue?.Invoke(this, EventArgs.Empty);
            }
        }

        private void InitQueue(SpaceshipSO[] spaceships)
        {
            waitingQueue.Clear();
            done.Clear();
            foreach (SpaceshipSO so in spaceships) 
            {
                Spaceship newSpaceship = GameObject.Instantiate(spaceshipPrefab, transform).GetComponent<Spaceship>();
                newSpaceship.Init(so);
                waitingQueue.Add(newSpaceship);
            }
            OnActiveSpaceshipChange?.Invoke(this, Current);
        }
    }
}

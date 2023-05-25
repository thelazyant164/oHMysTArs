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
        [Header("Spaceship")]
        [SerializeField]
        private Queue.Queue queue;
        [SerializeField]
        private GameObject spaceshipPrefab;
        [Space]

        [Header("State")]
        [SerializeField]
        private List<Spaceship> waitingQueue;
        public List<Spaceship> WaitingQueue => waitingQueue;
        [SerializeField]
        private List<Spaceship> done;
        public List<Spaceship> Done => done;
        public Spaceship Current => waitingQueue.FirstOrDefault();
        private LevelManager levelManager;
        public event EventHandler<Spaceship> OnDoneServing;
        public event EventHandler<Spaceship> OnActiveSpaceshipChange;
        public event EventHandler OnEndQueue;

        public void Init()
        {
            levelManager = GameManager.Instance.LevelManager;
            levelManager.OnStart += (object sender, Level.Level level) => InitQueue(level.Queue);
            levelManager.OnFinish += (object sender, Level.Level level) => StopQueue();
        }

        public void Next()
        {
            Spaceship last = Current;
            if (last != null)
            {
                waitingQueue.RemoveAt(0);
                done.Add(last);
                OnDoneServing?.Invoke(this, last);
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
            int i = 0;
            foreach (SpaceshipSO so in spaceships) 
            {
                Spaceship newSpaceship = GameObject.Instantiate(spaceshipPrefab, queue.transform).GetComponent<Spaceship>();
                newSpaceship.Init(so, queue.GetPosition(i), this);
                waitingQueue.Add(newSpaceship);
                i++;
            }
            OnActiveSpaceshipChange?.Invoke(this, Current);
        }

        private void StopQueue()
        {
            if (Current == null) return;
            Current.StopServing();
        }
    }
}

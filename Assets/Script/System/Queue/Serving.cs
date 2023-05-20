using Com.oHMysTArs.Spaceship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Queue
{
    public sealed class Serving : QueueState
    {
        private SpaceshipManager queue;
        private float remainingTime;

        public Serving(QueueSystem queueSystem) : base(queueSystem) 
        {
            queue = GameManager.Instance.SpaceshipManager;
            remainingTime = queueSystem.ServingTime;
        }

        public override IEnumerator Start()
        {
            while (remainingTime > 0) 
            { 
                remainingTime -= Time.deltaTime;
                yield return null;
            }
            queueSystem.SetState(new Failed(queueSystem));
        }

        public override void Terminate()
        {
            queue.Current.Record(remainingTime);
            queue.Next();
        }
    }
}

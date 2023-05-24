using Com.oHMysTArs.Spaceship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Queue
{
    public abstract class QueueState : State
    {
        protected SpaceshipManager queue;
        protected QueueSystem queueSystem => (QueueSystem)stateMachine;

        public QueueState(QueueSystem queueSystem) : base(queueSystem) 
        {
            queue = GameManager.Instance.SpaceshipManager;
        }
    }
}

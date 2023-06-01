using Com.oHMysTArs.Spaceship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Queue
{
    public sealed class QueueSystem : FiniteStateMachine
    {
        [SerializeField]
        private float servingTime = 10;
        public float ServingTime => servingTime;

        public QueueState CurrentQueueState => (QueueState)CurrentState;
    }
}

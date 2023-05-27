using Com.oHMysTArs.Spaceship;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Queue
{
    public sealed class QueueSystem : FiniteStateMachine
    {
        //private enum QueueStateEnum
        //{
        //    Waiting,
        //    Serving,
        //    Succeed,
        //    Fail,
        //    Undefined
        //}

        [SerializeField]
        private float servingTime = 10;
        public float ServingTime => servingTime;

        //[SerializeField]
        //private QueueStateEnum currentState;

        public QueueState CurrentQueueState => (QueueState)CurrentState;

        //private void FixedUpdate()
        //{
        //    currentState = CurrentQueueState is Waiting ? QueueStateEnum.Waiting 
        //        : CurrentQueueState is Serving ? QueueStateEnum.Serving
        //        : CurrentQueueState is Succeed ? QueueStateEnum.Succeed
        //        : CurrentQueueState is Failed ? QueueStateEnum.Fail : QueueStateEnum.Undefined;
        //}
    }
}

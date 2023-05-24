using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Queue
{
    public sealed class Succeed : QueueState
    {
        public Succeed(QueueSystem queueSystem) : base(queueSystem) 
        {
            queueSystem.Spaceship.PlaySucceedAnimation();
        }

        public override IEnumerator Start()
        {
            yield break;
        }

        public override void Terminate()
        {
        }
    }
}

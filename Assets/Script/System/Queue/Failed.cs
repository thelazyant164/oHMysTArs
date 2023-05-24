using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Queue
{
    public sealed class Failed : QueueState
    {
        public Failed(QueueSystem queueSystem) : base(queueSystem) 
        {
            queueSystem.Spaceship.PlayFailAnimation();
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

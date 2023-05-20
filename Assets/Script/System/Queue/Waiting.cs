using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Queue
{
    public sealed class Waiting : QueueState
    {
        public Waiting(QueueSystem queueSystem) : base(queueSystem) { }

        public override IEnumerator Start()
        {
            yield break;
        }

        public override void Terminate()
        {
            
        }
    }
}

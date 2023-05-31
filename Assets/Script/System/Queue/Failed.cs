using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Queue
{
    public sealed class Failed : QueueState
    {
        public Failed(QueueSystem queueSystem) : base(queueSystem) { }

        public override IEnumerator Start()
        {
            queue.Next();
            yield break;
        }

        public override void Terminate()
        {
        }
    }
}

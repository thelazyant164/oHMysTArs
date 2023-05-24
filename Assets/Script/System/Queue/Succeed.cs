using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Queue
{
    public sealed class Succeed : QueueState
    {
        public Succeed(QueueSystem queueSystem) : base(queueSystem) { }

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

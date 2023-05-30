using Com.oHMysTArs.Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.Tutorial
{
    public sealed class DrawTutorial : ActionTutorial
    {
        private bool correct = false;
        public override bool Complete => correct;

        protected override void Start()
        {
            DrawHistory history = GameManager.Instance.DrawHistory;
            history.OnDraw += (object sender, bool correct) => this.correct = correct;
            base.Start();
        }
    }
}

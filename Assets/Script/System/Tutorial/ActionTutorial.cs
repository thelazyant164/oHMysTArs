using Com.oHMysTArs.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Tutorial
{
    public abstract class ActionTutorial : TutorialContent
    {
        public override bool Complete => CheckCondition();

        protected virtual bool CheckCondition() => false;
    }
}

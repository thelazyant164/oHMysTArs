using Com.oHMysTArs.Input;
using Com.oHMysTArs.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Tutorial
{
    public sealed class InfoTutorial : TutorialContent
    {
        private bool hasClicked = false;
        private bool hasReleased = false;
        public override bool Complete => hasClicked && hasReleased;

        public override void Play()
        {
            InputManager.Instance.OnMouseDown += (object sender, EventArgs e) => 
            { 
                hasClicked = true;
                hasReleased = false;
            };
            InputManager.Instance.OnMouseUp += (object sender, EventArgs e) =>
            {
                hasReleased = true;
            };
            base.Play();
        }
    }
}

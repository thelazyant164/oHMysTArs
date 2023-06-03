using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.Tutorial
{
    public sealed class OpenCheatSheetTutorial : ActionTutorial
    {
        [SerializeField]
        private GameObject cheatSheet;
        [SerializeField]
        private ParticleSystem emphasis;
        private Image cheatSheetGraphic;

        private void Start()
        {
            cheatSheetGraphic = cheatSheet.GetComponentInChildren<Image>();
        }

        public override void Play()
        {
            base.Play();
            emphasis.Play();
        }

        public override void Close()
        {
            base.Close();
            emphasis.Stop();
        }

        protected override bool CheckCondition() => cheatSheetGraphic.IsActive();
    }
}

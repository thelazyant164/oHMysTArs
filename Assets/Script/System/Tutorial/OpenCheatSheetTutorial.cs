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
        private Image cheatSheetGraphic;

        protected override void Start()
        {
            cheatSheetGraphic = cheatSheet.GetComponentInChildren<Image>();
            base.Start();
        }

        protected override bool CheckCondition() => cheatSheetGraphic.IsActive();
    }
}

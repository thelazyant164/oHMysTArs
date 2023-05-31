using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.Tutorial
{
    public sealed class CloseCheatSheetTutorial : ActionTutorial
    {
        [SerializeField]
        private GameObject cheatSheet;
        private Image cheatSheetGraphic;

        private void Start()
        {
            cheatSheetGraphic = cheatSheet.GetComponentInChildren<Image>();
        }

        protected override bool CheckCondition() => !cheatSheetGraphic.IsActive();
    }
}

using Com.oHMysTArs.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.UI
{
    public sealed class CheatSheet : MonoBehaviour
    {
        [SerializeField]
        private GameObject cheatSheet;
        private Image cheatSheetGraphic;
        private Image cheatSheetTrigger;
        private InputManager inputManager;
        public event EventHandler OnHover;
        public event EventHandler OnStopHover;

        private void Awake() => cheatSheetTrigger = GetComponentInChildren<Image>();

        private void Start()
        {
            inputManager = InputManager.Instance;
            cheatSheetGraphic = cheatSheet.GetComponentInChildren<Image>();
            OnHover += OpenCheatsheet;
            CloseCheatsheet(this, EventArgs.Empty);
        }

        public void FixedUpdate()
        {
            if (TryHoverCheatSheet(out GameObject cheatSheet))
            {
                OnHover?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnStopHover?.Invoke(this, EventArgs.Empty);
            }
        }

        private bool TryHoverCheatSheet(out GameObject cheatSheet)
        {
            cheatSheet = null;
            if (inputManager.TryGetHoverElement(true, out GameObject hovering))
            {
                cheatSheet = hovering == cheatSheetTrigger.gameObject ? this.cheatSheet : null;
            }
            return cheatSheet != null;
        }

        private void OpenCheatsheet(object sender, EventArgs e)
        {
            OnHover -= OpenCheatsheet;
            OnStopHover += CloseCheatsheet;
            cheatSheetGraphic.enabled = true; 
        }

        private void CloseCheatsheet(object sender, EventArgs e) 
        {
            OnStopHover -= CloseCheatsheet;
            OnHover += OpenCheatsheet;
            cheatSheetGraphic.enabled = false; 
        }
    }
}

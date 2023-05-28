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
        [Header("SFX")]
        [SerializeField]
        private AudioClip openCheatSheetSFX;
        [SerializeField]
        private AudioClip closeCheatSheetSFX;
        [Space]

        [SerializeField]
        private GameObject cheatSheet;
        private Image cheatSheetGraphic;
        private Image cheatSheetTrigger;
        private InputManager inputManager;
        public event EventHandler OnHover;
        public event EventHandler OnStopHover;
        private AudioSource audioSource;

        private void Awake() => cheatSheetTrigger = GetComponentInChildren<Image>();

        private void Start()
        {
            audioSource = UIManager.Instance.UIAudio;
            inputManager = InputManager.Instance;
            cheatSheetGraphic = cheatSheet.GetComponentInChildren<Image>();
            cheatSheetGraphic.enabled = false;
            OnHover += OpenCheatsheet;
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
            audioSource.PlayOneShot(openCheatSheetSFX);
            OnHover -= OpenCheatsheet;
            OnStopHover += CloseCheatsheet;
            cheatSheetGraphic.enabled = true; 
        }

        private void CloseCheatsheet(object sender, EventArgs e) 
        {
            audioSource.PlayOneShot(closeCheatSheetSFX);
            OnStopHover -= CloseCheatsheet;
            OnHover += OpenCheatsheet;
            cheatSheetGraphic.enabled = false; 
        }
    }
}

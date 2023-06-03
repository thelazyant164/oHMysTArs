using Com.oHMysTArs.Input;
using Com.oHMysTArs.Tutorial;
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
        private ParticleSystem emphasis;
        private Image cheatSheetGraphic;
        private Image cheatSheetTrigger;
        private InputManager inputManager;
        public event EventHandler OnHover;
        public event EventHandler OnStopHover;
        private AudioSource audioSource;
        private bool active = true;
        private InputManager.RaycastLayer layer = InputManager.RaycastLayer.UI;

        private void Awake() 
        { 
            cheatSheetTrigger = GetComponentInChildren<Image>();
            emphasis = GetComponentInChildren<ParticleSystem>();
        }

        private void Start()
        {
            audioSource = UIManager.Instance.UIAudio;
            inputManager = InputManager.Instance;
            cheatSheetGraphic = cheatSheet.GetComponentInChildren<Image>();
            cheatSheetGraphic.enabled = false;
            OnHover += OpenCheatsheet;
            GameManager.Instance.LevelManager.OnFinish += (object sender, Level.Level level) => active = false;
            TutorialManager tutorialManager = GameManager.Instance.TutorialManager;
            if (tutorialManager != null)
            {
                tutorialManager.OnStart += (object sender, TutorialContent tutorial) => 
                {
                    if (tutorial is DrawTutorial)
                    {
                        layer = InputManager.RaycastLayer.Focus;
                    }
                    active = !tutorial.DisableCheatSheet; 
                };
                tutorialManager.OnComplete += (object sender, TutorialContent tutorial) => 
                {
                    if (tutorial is DrawTutorial)
                    {
                        layer = InputManager.RaycastLayer.UI;
                    }
                    active = true; 
                };
            }
        }

        public void Update()
        {
            if (!active) return;
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
            if (inputManager.TryGetHoverElement(layer, out GameObject hovering))
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

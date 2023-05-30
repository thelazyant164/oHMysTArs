using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.Diagnostics.Tracing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Com.oHMysTArs.UI
{
    public class PausePopUp : PopUp
    {
        [SerializeField]
        private Button resumeButton;
        [SerializeField]
        private Button menuButton;

        private void Awake()
        {
            resumeButton.onClick.AddListener(() => 
            {
                if (!GameManager.Instance.InTutorialMode) GameManager.Instance.TogglePause(false);
                Hide(); 
            });
            menuButton.onClick.AddListener(() => 
                PopUpManager.Instance.ShowConfirm("PROGRESS WILL BE LOST", 
                    () => SceneManager.LoadSceneAsync("MenuScene")));
        }
    }
}

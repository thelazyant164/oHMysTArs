using Com.oHMysTArs.Assessment;
using Com.oHMysTArs.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.oHMysTArs.Level
{
    public sealed class LevelButton : MonoBehaviour
    {
        [Header("Flash effect")]
        [SerializeField]
        private float lockedOpacity = .1f;
        [SerializeField]
        private float newUnlockMinOpacity = .5f;
        [SerializeField]
        private float newUnlockMaxOpacity = 1f;
        [SerializeField]
        private float newUnlockFlashInterval = .2f;
        [SerializeField]
        private float newUnlockFlashDuration = .1f;
        [Space]

        [Header("Element")]
        [SerializeField]
        private Image frame;
        [SerializeField]
        private Image locked;
        [SerializeField]
        private TextMeshProUGUI levelName;
        [SerializeField]
        private StarRating result;

        private Button button;

        private void Awake()
        {
            button = GetComponentInChildren<Button>();
        }

        public void Init(LevelResult level)
        {
            locked.gameObject.SetActive(false);
            levelName.SetText(level.Name);
            result.Init((Rating)level.Rating);
            button.onClick.AddListener(() => 
            {
                LevelSelected.Instance.SelectLevel(level.Name);
                SceneManager.LoadSceneAsync("GameScene");
            });
        }

        public void Init(Level lockedLevel)
        {
            locked.gameObject.SetActive(true);
            levelName.gameObject.SetActive(false);
            result.gameObject.SetActive(false);
            button.interactable = false;
            StartCoroutine(frame.FadeTo(lockedOpacity, 1f));
            StartCoroutine(locked.FadeTo(lockedOpacity, 1f));
        }

        public void Init(string unlockedLevel)
        {
            locked.gameObject.SetActive(false);
            levelName.SetText(unlockedLevel);
            result.gameObject.SetActive(false);
            button.onClick.AddListener(() =>
            {
                LevelSelected.Instance.SelectLevel(unlockedLevel);
                SceneManager.LoadSceneAsync("GameScene");
            });
            StartCoroutine(frame.Flash(newUnlockMaxOpacity, newUnlockMinOpacity, newUnlockFlashInterval, newUnlockFlashDuration));
            StartCoroutine(locked.Flash(newUnlockMaxOpacity, newUnlockMinOpacity, newUnlockFlashInterval, newUnlockFlashDuration));
        }
    }
}

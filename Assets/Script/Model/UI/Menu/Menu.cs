using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.oHMysTArs.UI
{
    public sealed class Menu : MonoBehaviour
    {
        [SerializeField]
        private Button playButton;
        [SerializeField]
        private Button collectibleButton;
        [SerializeField]
        private Button creditButton;
        [SerializeField]
        private Button quitButton;
        [SerializeField]
        private ParticleSystem stars;

        private void Awake()
        {
            playButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("LevelSelectionScene"));
            collectibleButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("CollectibleScene"));
            creditButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("CreditScene"));
            quitButton.onClick.AddListener(() => Application.Quit());
        }

        private void Start()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            stars.Play();
        }
    }
}

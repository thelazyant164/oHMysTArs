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
        private Button quitButton;

        private void Awake()
        {
            playButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("GameScene"));
            quitButton.onClick.AddListener(() => Application.Quit());
        }
    }
}

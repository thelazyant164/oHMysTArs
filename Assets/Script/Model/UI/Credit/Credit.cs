using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.oHMysTArs.UI
{
    public sealed class Credit : MonoBehaviour
    {
        [SerializeField]
        private Button backButton;

        private void Awake()
        {
            backButton.onClick.AddListener(() => SceneManager.LoadSceneAsync("MenuScene"));
        }

        private void Start()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }
    }
}

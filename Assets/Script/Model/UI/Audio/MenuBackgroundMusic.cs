using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.oHMysTArs.UI
{
    public sealed class MenuBackgroundMusic : Singleton<MenuBackgroundMusic>
    {
        private AudioSource bgm;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(this.gameObject);
            bgm = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnLoadScene;
        }

        private void OnLoadScene(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "GameScene")
            {
                bgm.Stop();
            }
            else if (!bgm.isPlaying)
            {
                bgm.Play();
            }
        }
    }
}

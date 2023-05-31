using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Level
{
    public sealed class LevelSelected : Singleton<LevelSelected>
    {
        [SerializeField]
        private string selected;
        public string Selected => selected;

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
        }

        public void SelectLevel(string name) => selected = name;
    }
}

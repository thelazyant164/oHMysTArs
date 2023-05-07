using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs
{
    public sealed class GameManager : Singleton<GameManager>
    {
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}

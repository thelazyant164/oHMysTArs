using Com.oHMysTArs.Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs
{
    public sealed class GameManager : Singleton<GameManager>
    {
        public DrawHistory DrawHistory { get; private set; }

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

            DrawHistory = GetComponentInChildren<DrawHistory>();
        }
    }
}

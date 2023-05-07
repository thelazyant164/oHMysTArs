using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Input
{
    public sealed class InputManager : Singleton<InputManager>
    {
        public PointSelectionManager PointSelectionManager { get; private set; }

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

            PointSelectionManager = GetComponentInChildren<PointSelectionManager>();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Com.oHMysTArs.Input
{
    public sealed class InputManager : Singleton<InputManager>
    {
        public PointSelectionManager PointSelectionManager { get; private set; }
        public PointSelectionCache PointSelectionCache { get; private set; }

        private event EventHandler _onMouseDown;
        private event EventHandler _onMouseUp;
        public event EventHandler OnMouseDown;
        public event EventHandler OnMouseUp;

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
            PointSelectionCache = GetComponentInChildren<PointSelectionCache>();
            _onMouseDown += OnMouseDownCallback;
        }

        private void Update()
        {
            if (!IsLMBDown())
            {
                _onMouseUp?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                _onMouseDown?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool IsLMBDown() => UnityEngine.Input.GetMouseButtonDown(0);

        public Vector2 GetMouseScreenPosition()
        {
            return UnityEngine.Input.mousePosition;
        }

        private void OnMouseDownCallback(object sender, EventArgs e)
        {
            OnMouseDown?.Invoke(this, EventArgs.Empty);
            _onMouseDown -= OnMouseDownCallback;
            _onMouseUp -= OnMouseUpCallback;
            _onMouseUp += OnMouseUpCallback;
        }

        private void OnMouseUpCallback(object sender, EventArgs e)
        {
            OnMouseUp?.Invoke(this, EventArgs.Empty);
            _onMouseDown -= OnMouseDownCallback;
            _onMouseDown += OnMouseDownCallback;
            _onMouseUp -= OnMouseUpCallback;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

namespace Com.oHMysTArs.Input
{
    public sealed class InputManager : Singleton<InputManager>
    {
        [SerializeField]
        private GraphicRaycaster raycaster;
        public PointSelectionManager PointSelectionManager { get; private set; }
        public PointSelectionCache PointSelectionCache { get; private set; }
        public bool IsMouseDown { get; private set; }

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
        }

        private void Update()
        {
            if (IsLMBDown())
            {
                IsMouseDown = true;
                OnMouseDown?.Invoke(this, EventArgs.Empty);
            }
            if (IsLMBUp())
            {
                IsMouseDown = false;
                OnMouseUp?.Invoke(this, EventArgs.Empty);
            }
        }

        private bool IsLMBDown() => UnityEngine.Input.GetMouseButtonDown(0);

        private bool IsLMBUp() => UnityEngine.Input.GetMouseButtonUp(0);

        public Vector3 GetMouseWorldPosition() => Camera.main.ScreenToWorldPoint(GetMouseScreenPosition());

        public Vector2 GetMouseScreenPosition() => UnityEngine.Input.mousePosition;

        public bool TryGetHoverUIElement(out GameObject hovering)
        {
            hovering = null;
            List<RaycastResult> result = new();
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = GetMouseScreenPosition();
            raycaster.Raycast(pointer, result);
            if (result.Any()) hovering = result.First().gameObject;
            return hovering != null;
        }
    }
}

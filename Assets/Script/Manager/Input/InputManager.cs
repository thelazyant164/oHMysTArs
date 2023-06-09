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
        public enum RaycastLayer
        {
            Game = 0,
            Focus = 1,
            UI = 2
        }

        [SerializeField]
        private GraphicRaycaster UIRaycaster;
        [SerializeField]
        private GraphicRaycaster gameRaycaster;
        [SerializeField]
        private GraphicRaycaster focusRaycaster;
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

        public bool TryGetHoverElement(RaycastLayer layer, out GameObject hovering)
        {
            hovering = null;
            List<RaycastResult> result = new();
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = GetMouseScreenPosition();
            GraphicRaycaster raycaster;
            switch (layer)
            {
                case RaycastLayer.Game:
                    raycaster = gameRaycaster; 
                    break;
                case RaycastLayer.Focus: 
                    raycaster = focusRaycaster; 
                    break;
                case RaycastLayer.UI: 
                    raycaster = UIRaycaster; 
                    break;
                default: 
                    Debug.LogError($"Unrecognized layer: {layer}");
                    throw new Exception("Cannot try get hover element with invalid layer");
            }
            raycaster.Raycast(pointer, result);
            if (result.Any()) hovering = result.First().gameObject;
            return hovering != null;
        }
    }
}

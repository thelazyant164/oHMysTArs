using Com.oHMysTArs.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Input
{
    public sealed class PointSelectionManager : MonoBehaviour
    {
        [Header("Raycast configurations")]
        [SerializeField]
        private LayerMask pointLayer;

        private InputManager inputManager;
        public event EventHandler<Point> OnHover;
        public event EventHandler<Point> OnSelect;

        private void Start()
        {
            inputManager = InputManager.Instance;
        }

        public void Update()
        {
            if (!TrySelectPoint(out Point point) || point.Active) return;
            if (inputManager.IsLMBDown())
            {
                OnSelect?.Invoke(this, point);
            }
            else
            {
                OnHover?.Invoke(this, point);
            }
        }

        private bool TrySelectPoint(out Point point)
        {
            Ray ray = Camera.main.ScreenPointToRay(inputManager.GetMouseScreenPosition());
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, pointLayer);
            point = hit.collider?.GetComponentInParent<Point>();
            return point != null;
        }
    }
}

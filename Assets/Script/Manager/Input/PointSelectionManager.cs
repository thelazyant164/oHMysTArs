using Com.oHMysTArs.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.Input
{
    public sealed class PointSelectionManager : MonoBehaviour
    {
        private InputManager inputManager;
        public event EventHandler OnStopHover;
        public event EventHandler<Point> OnHover;
        public event EventHandler<Point> OnSelect;

        private void Start()
        {
            inputManager = InputManager.Instance;
        }

        public void Update()
        {
            OnStopHover?.Invoke(this, EventArgs.Empty);
            if (!TrySelectPoint(out Point point) || point.Active) return;
            if (inputManager.IsMouseDown)
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
            point = null;
            if (inputManager.TryGetHoverUIElement(out GameObject hoveringPoint))
            {
                point = hoveringPoint.GetComponentInParent<Point>();
            }
            return point != null;
        }
    }
}

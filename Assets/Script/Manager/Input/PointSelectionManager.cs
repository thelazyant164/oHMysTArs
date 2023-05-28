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
        public event EventHandler<Point> OnHover;
        public event EventHandler<Point> OnStopHover;
        public event EventHandler<Point> OnSelect;
        private Point lastHovering;

        private void Start()
        {
            inputManager = InputManager.Instance;
        }

        public void Update()
        {
            if (!TrySelectPoint(out Point point) || point.Active) 
            {
                OnStopHover?.Invoke(this, lastHovering);
                lastHovering = null;
                return; 
            }
            if (lastHovering != point) 
            {
                OnStopHover?.Invoke(this, lastHovering);
                OnHover?.Invoke(this, point);
                lastHovering = point;
            }
            if (inputManager.IsMouseDown)
            {
                OnSelect?.Invoke(this, point);
            }
        }

        private bool TrySelectPoint(out Point point)
        {
            point = null;
            if (inputManager.TryGetHoverElement(false, out GameObject hoveringPoint))
            {
                point = hoveringPoint.GetComponentInParent<Point>();
            }
            return point != null;
        }
    }
}

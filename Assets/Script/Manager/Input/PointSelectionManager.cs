using Com.oHMysTArs.Grid;
using Com.oHMysTArs.Level;
using Com.oHMysTArs.Tutorial;
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
        private bool active = true;
        private InputManager.RaycastLayer layer = InputManager.RaycastLayer.Game;

        private void Start()
        {
            inputManager = InputManager.Instance;
            GameManager.Instance.LevelManager.OnFinish += (object sender, Level.Level level) => active = false;
            TutorialManager tutorialManager = GameManager.Instance.TutorialManager;
            if (tutorialManager != null)
            {
                tutorialManager.OnStart += (object sender, TutorialContent tutorial) => 
                { 
                    if (tutorial is DrawTutorial)
                    {
                        layer = InputManager.RaycastLayer.Focus;
                    }
                    active = !tutorial.DisableDrawing; 
                };
                tutorialManager.OnComplete += (object sender, TutorialContent tutorial) => 
                { 
                    if (tutorial is DrawTutorial)
                    {
                        layer = InputManager.RaycastLayer.Game;
                    }
                    active = true; 
                };
            }
        }

        public void Update()
        {
            if (!active || !TrySelectPoint(out Point point) || point.Active) 
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
            if (inputManager.TryGetHoverElement(layer, out GameObject hoveringPoint))
            {
                point = hoveringPoint.GetComponentInParent<Point>();
            }
            return point != null;
        }
    }
}

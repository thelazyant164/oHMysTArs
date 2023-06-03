using Com.oHMysTArs.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Grid
{
    public sealed class Line : MonoBehaviour
    {
        private Vector3 start;
        private LineRenderer lineRenderer;
        private InputManager input;
        private bool dynamic = false;

        private void Awake()
        {
            lineRenderer = GetComponentInChildren<LineRenderer>();
            lineRenderer.positionCount = 2;
        }

        private void Update()
        {
            if (!dynamic) return;
            Setup(start, input.GetMouseWorldPosition());
        }

        public void Setup(Vector3 start, Vector3 end)
        {
            this.start = start;
            lineRenderer.SetPositions(new Vector3[]
            {
                new Vector3(start.x, start.y),
                new Vector3(end.x, end.y),
            });
        }

        public void Attach(Vector3 start) 
        { 
            dynamic = true;
            input = InputManager.Instance;
            Setup(start, input.GetMouseWorldPosition());
        }
    }
}

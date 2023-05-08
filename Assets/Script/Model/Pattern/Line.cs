using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Grid
{
    public sealed class Line : MonoBehaviour
    {
        private Point start;
        private Point end;
        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponentInChildren<LineRenderer>();
            lineRenderer.positionCount = 2;
        }

        public void Setup(Point start, Point end)
        {
            this.start = start;
            this.end = end;
            
            lineRenderer.SetPositions(new Vector3[] 
            { 
                start.transform.position,
                end.transform.position,
            });
        }
    }
}

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

        public void Setup(Point start, Point end)
        {
            this.start = start;
            this.end = end;

            lineRenderer = GetComponentInChildren<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(new Vector3[] 
            { 
                start.transform.position,
                end.transform.position,
            });
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

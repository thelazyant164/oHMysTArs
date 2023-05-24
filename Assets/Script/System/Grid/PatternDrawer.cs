using Com.oHMysTArs.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Com.oHMysTArs.Input.PointSelectionCache;

namespace Com.oHMysTArs.Grid
{
    public sealed class PatternDrawer : MonoBehaviour
    {
        [Header("Pattern drawing configurations")]
        [SerializeField]
        private GameObject linePrefab;

        private InputManager input;
        private PointSelectionCache cache;
        private readonly List<Line> lines = new();
        private Line tempLine;

        // Start is called before the first frame update
        void Start()
        {
            input = InputManager.Instance;
            cache = input.PointSelectionCache;
            cache.OnSelectNew += OnSelectNew;
            cache.OnDrawSegment += OnDrawSegment;
            input.OnMouseUp += OnMouseUp;
        }

        private void OnSelectNew(object sender, Point point)
        {
            if (tempLine == null) tempLine = Instantiate(linePrefab, transform).GetComponent<Line>();
            tempLine.Attach(point.transform.position);
        }

        private void OnDrawSegment(object sender, Segment segment)
        {
            GameObject newSegment = Instantiate(linePrefab, transform);
            Line line = newSegment.GetComponent<Line>();
            line.Setup(segment.Start.transform.position, segment.End.transform.position);
            lines.Add(line);
        }

        private void OnMouseUp(object sender, System.EventArgs e)
        {
            foreach (Line line in lines)
            {
                GameObject.Destroy(line.gameObject);
            }
            lines.Clear();
            if (tempLine != null) GameObject.Destroy(tempLine.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.oHMysTArs.Grid;
using System;
using System.Linq;

namespace Com.oHMysTArs.Input
{
    public sealed class PointSelectionCache : MonoBehaviour
    {
        public readonly struct Segment
        {
            public readonly Point Start;
            public readonly Point End;

            public Segment(Point start, Point end)
            {
                this.Start = start;
                this.End = end;
            }
        }

        private PointSelectionManager selectionManager;

        [Header("State")]
        [SerializeField]
        private readonly List<Point> selected = new();
        [SerializeField]
        private readonly List<Segment> drawn = new();

        public event EventHandler<Segment> OnDrawSegment;

        private void Start()
        {
            selectionManager = InputManager.Instance.PointSelectionManager;
            selectionManager.OnSelect += SelectPoint;
        }

        private void SelectPoint(object sender, Point point)
        {
            if (selected.Contains(point)) return;
            Point last = selected.Last();
            Segment newSegment = new Segment(last, point);
            if (!drawn.Contains(newSegment)) 
            {
                drawn.Add(newSegment);
                OnDrawSegment?.Invoke(sender, newSegment);
            }
            selected.Add(point);
        }
    }
}

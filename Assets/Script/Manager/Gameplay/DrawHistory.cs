using System.Collections;
using System.Collections.Generic;
using Com.oHMysTArs.Grid;
using Com.oHMysTArs.Input;
using UnityEngine;

namespace Com.oHMysTArs.Pattern
{
    public sealed class DrawHistory : MonoBehaviour
    {
        [SerializeField]
        private List<Pattern> history;
        private PointSelectionCache cache;

        private void Start()
        {
            cache = InputManager.Instance.PointSelectionCache;
            cache.OnDrawPattern += RecordPattern;
        }

        private void RecordPattern(object sender, List<Point> points)
        {
            Pattern pattern = Pattern.Record(points);
            history.Add(GameObject.Instantiate(pattern));
        }
    }
}

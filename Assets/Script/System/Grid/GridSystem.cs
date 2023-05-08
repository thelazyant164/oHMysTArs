using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Grid
{
    public sealed class GridSystem : MonoBehaviour
    {
        [Serializable]
        public struct Coordinate
        {
            public int Column;
            public int Row;

            public Coordinate(int column, int row)
            {
                this.Column = column;
                this.Row = row;
            }

            public static bool Match(Coordinate first, Coordinate second) 
                => first.Column == second.Column 
                   && first.Row == second.Row;
        }

        [Header("Grid configurations")]
        [SerializeField]
        private int columnCount = 3;
        [SerializeField]
        private int rowCount = 3;
        [Space]

        [Header("Grid layout")]
        [SerializeField]
        private int pointDistance = 5;
        [SerializeField]
        private Transform rootPoint;
        [Space]

        [Header("Grid prefabs")]
        [SerializeField]
        private GameObject pointPrefab;

        private readonly List<Point> points = new();

        private void Awake()
        {
            for (int i = 0; i < columnCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    GameObject newPoint = Instantiate(pointPrefab, transform);
                    Point point = newPoint.GetComponent<Point>();
                    point.transform.position = rootPoint.position + new Vector3(j * pointDistance, i * pointDistance);
                    point.Setup(new Coordinate(i, j));
                    points.Add(point);
                }
            }
        }

        private void Start()
        {
            
        }
    }
}

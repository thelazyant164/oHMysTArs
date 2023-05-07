using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Grid
{
    public sealed class GridSystem : MonoBehaviour
    {
        [Header("Grid configurations")]
        [SerializeField]
        private int columnCount = 3;
        [SerializeField]
        private int rowCount = 3;
        [Space]

        [Header("Grid prefabs")]
        [SerializeField]
        private GameObject pointPrefab;
        [SerializeField]
        private GameObject linePrefab;

        private readonly List<Point> points = new();

        private void Awake()
        {
            for (int i = 0; i < columnCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    GameObject newPoint = Instantiate(pointPrefab, transform);
                    newPoint.GetComponent<Point>().Setup(i, j);
                }
            }
        }
    }
}

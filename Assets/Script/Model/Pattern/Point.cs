using Com.oHMysTArs.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Com.oHMysTArs.Grid.GridSystem;

namespace Com.oHMysTArs.Grid
{
    public sealed class Point : MonoBehaviour
    {
        [Header("Dot configurations")]
        [SerializeField]
        private Sprite star;
        [SerializeField]
        private Sprite starSelected;
        [Space]

        [Header("State")]
        [SerializeField]
        private bool active;
        public bool Active => active;

        public Coordinate Coordinate { get; private set; }
        private Image image;
        private InputManager inputManager;
        private PointSelectionManager selectionManager;

        private void Awake()
        {
            image = GetComponentInChildren<Image>();
        }

        private void Start()
        {
            inputManager = InputManager.Instance;
            selectionManager = inputManager.PointSelectionManager;
            inputManager.OnMouseUp += (object sender, EventArgs e) => Deactivate();
            selectionManager.OnSelect += (object sender, Point point) =>
            {
                if (this == point) Activate();
            };
            selectionManager.OnHover += (object sender, Point point) =>
            {
                if (Active || this != point) return;
                Select();
            };
            selectionManager.OnStopHover += (object sender, EventArgs e) =>
            {
                if (Active) return;
                Deselect();
            };
        }

        public void Setup(GridSystem.Coordinate coordinate)
        {
            this.Coordinate = coordinate;
        }

        public void Activate()
        {
            active = true;
            Select();
        }

        public void Deactivate()
        {
            active = false;
            Deselect();
        }

        public void Select() => image.sprite = starSelected;

        public void Deselect() => image.sprite = star;

        public override string ToString() => $"Point {Coordinate.Column}:{Coordinate.Row}";
    }
}

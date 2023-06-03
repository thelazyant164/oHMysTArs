using Com.oHMysTArs.Input;
using Com.oHMysTArs.UI;
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
        [Header("Texture")]
        [SerializeField]
        private Sprite star;
        [SerializeField]
        private Sprite starSelected;
        [Space]

        [Header("State")]
        [SerializeField]
        private bool active;
        public bool Active => active;
        [Space]

        [Header("SFX")]
        [SerializeField]
        private AudioClip twinkleSelect;
        [SerializeField]
        private AudioClip clinkHover1;
        [SerializeField]
        private AudioClip clinkHover2;

        public Coordinate Coordinate { get; private set; }
        private Image image;
        private ParticleSystem twinkle;
        private InputManager inputManager;
        private PointSelectionManager selectionManager;

        private void Awake()
        {
            image = GetComponentInChildren<Image>();
            twinkle = GetComponentInChildren<ParticleSystem>();
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
                UIManager.Instance.UIAudio.PlayOneShot(UnityEngine.Random.Range(0, 1) == 0 ? clinkHover1 : clinkHover2, .25f);
                Select();
            };
            selectionManager.OnStopHover += (object sender, Point point) =>
            {
                if (Active || this != point) return;
                Deselect();
            };
        }

        public void Setup(GridSystem.Coordinate coordinate)
        {
            this.Coordinate = coordinate;
        }

        public void Activate()
        {
            UIManager.Instance.UIAudio.PlayOneShot(twinkleSelect, .25f);
            active = true;
            twinkle.Play();
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

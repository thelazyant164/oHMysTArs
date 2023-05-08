using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private int column;
        private int row;
        private SpriteRenderer spriteRenderer;

        public void Setup(int column, int row)
        {
            this.column = column;
            this.row = row;
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void Select()
        {
            active = true;
            spriteRenderer.sprite = starSelected;
        }

        public void Deselect()
        {
            active = false;
            spriteRenderer.sprite = star;
        }
    }
}

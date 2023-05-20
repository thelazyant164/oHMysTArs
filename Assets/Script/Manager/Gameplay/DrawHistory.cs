using System;
using System.Collections;
using System.Collections.Generic;
using Com.oHMysTArs.Grid;
using Com.oHMysTArs.Input;
using Com.oHMysTArs.Spaceship;
using UnityEngine;

namespace Com.oHMysTArs.Pattern
{
    public sealed class DrawHistory : MonoBehaviour
    {
        [SerializeField]
        private List<Pattern> history;
        private PointSelectionCache cache;
        private Spaceship.Spaceship current;
        private SpaceshipManager spaceshipManager;
        public event EventHandler<bool> OnDraw;

        private void Start()
        {
            cache = InputManager.Instance.PointSelectionCache;
            cache.OnDrawPattern += RecordPattern;
            spaceshipManager.OnActiveSpaceshipChange += ActiveSpaceshipChange;
            spaceshipManager.OnEndQueue += EndQueue;
        }

        private void ActiveSpaceshipChange(object sender, Spaceship.Spaceship spaceship)
        {
            history.Clear();
            if (current != null) OnDraw -= current.Draw;
            OnDraw += spaceship.Draw;
            current = spaceship;
        }

        private void EndQueue(object sender, EventArgs e)
        {
            history.Clear();
            OnDraw -= current.Draw;
            current = null;
        }

        private void RecordPattern(object sender, List<Point> points)
        {
            Pattern pattern = Pattern.Record(points);
            history.Add(GameObject.Instantiate(pattern));
            OnDraw?.Invoke(this, Pattern.Match(pattern, current.Pattern));
        }
    }
}

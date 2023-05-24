using Com.oHMysTArs.Spaceship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.oHMysTArs.Queue
{
    public sealed class Queue : MonoBehaviour
    {
        [Header("Queue layout")]
        [SerializeField]
        private Transform servingPosition;
        [SerializeField]
        private Transform lastInLine;
        private Vector2 offset => lastInLine.position - servingPosition.position;
        private SpaceshipManager spaceshipManager;

        private void Start()
        {
            spaceshipManager = GameManager.Instance.SpaceshipManager;
            spaceshipManager.OnDoneServing += SendAway;
            spaceshipManager.OnActiveSpaceshipChange += Progress;
            spaceshipManager.OnEndQueue += (object sender, EventArgs e) 
                => spaceshipManager.OnActiveSpaceshipChange -= Progress;
        }

        private void SendAway(object sender, Spaceship.Spaceship spaceship)
        {
            if (spaceship.Succeed)
            {
                spaceship.PlaySucceedAnimation();
            }
            else
            {
                spaceship.PlayFailAnimation();
            }
        }

        private void Progress(object sender, Spaceship.Spaceship spaceship)
        {
            int order = 0;
            foreach (Spaceship.Spaceship waitingSpaceship in spaceshipManager.WaitingQueue)
            {
                waitingSpaceship.MoveTo(GetPosition(order));
                order++;
            }
        }

        public Vector2 GetPosition(int order) => (Vector2)servingPosition.position + offset * order;
    }
}

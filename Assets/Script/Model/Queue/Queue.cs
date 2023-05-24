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
        private Transform succeedPosition;
        [SerializeField]
        private Transform servingPosition;
        [SerializeField]
        private Transform failedPosition;
        [SerializeField]
        private int horizontalQueueGap;
        [SerializeField]
        private int verticalQueueGap;
        private Vector2 offset => new Vector2(horizontalQueueGap, - verticalQueueGap);
        private SpaceshipManager spaceshipManager;

        private void Start()
        {
            spaceshipManager = GameManager.Instance.SpaceshipManager;
            spaceshipManager.OnActiveSpaceshipChange += Progress;
            spaceshipManager.OnEndQueue += (object sender, EventArgs e) 
                => spaceshipManager.OnActiveSpaceshipChange -= Progress;
        }

        private void Progress(object sender, Spaceship.Spaceship spaceship)
        {
            spaceship.MoveTo(spaceship.Succeed ? succeedPosition.position : failedPosition.position);
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

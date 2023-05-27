using Com.oHMysTArs.Spaceship;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.Queue
{
    public sealed class Queue : MonoBehaviour
    {
        [Header("Info")]
        [SerializeField]
        private TextMeshProUGUI plate;
        [SerializeField]
        private Image scan;
        [Space]

        [Header("Flicker effect")]
        [SerializeField]
        private float minInterval = .1f;
        [SerializeField]
        private float maxInterval = .75f;
        [SerializeField]
        private float minDuration = .1f;
        [SerializeField]
        private float maxDuration = .15f;
        private Coroutine flickerEffect;
        [Space]

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
            spaceshipManager.OnEndQueue += (object sender, EventArgs e) =>
            {
                StopCoroutine(flickerEffect);
                plate.gameObject.SetActive(false);
                scan.gameObject.SetActive(false);
                spaceshipManager.OnActiveSpaceshipChange -= Progress;
            };
            flickerEffect = StartCoroutine(Flicker());
        }

        private IEnumerator Flicker()
        {
            while (true)
            {
                // Cooldown
                float interval = UnityEngine.Random.Range(minInterval, maxInterval);
                yield return new WaitForSeconds(interval);

                // Fade out
                float duration = UnityEngine.Random.Range(minDuration, maxDuration);
                StartCoroutine(scan.Fade(duration));
                StartCoroutine(plate.Fade(duration));
                yield return new WaitForSeconds(duration);
                // Fade back in
                StartCoroutine(scan.Fade(duration));
                StartCoroutine(plate.Fade(duration));
                yield return new WaitForSeconds(duration);
            }
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
            plate.SetText(spaceship.Plate);
            scan.SetTexture(spaceship.ScanTexture);
        }

        public Vector2 GetPosition(int order) => (Vector2)servingPosition.position + offset * order;
    }
}

using Com.oHMysTArs.Spaceship;
using Com.oHMysTArs.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        private AudioSource flickerAudioSource;
        [Space]

        [Header("Queue layout")]
        [SerializeField]
        private Transform servingPosition;
        [SerializeField]
        private Transform lastInLine;
        private Vector2 offset => lastInLine.position - servingPosition.position;
        private Vector3 scaleFactor => lastInLine.localScale - servingPosition.localScale;
        private SpaceshipManager spaceshipManager;
        private Static staticFrit;

        private void Awake()
        {
            staticFrit = GetComponentInChildren<Static>();
            flickerAudioSource = GetComponentInChildren<AudioSource>();
        }

        private void Start()
        {
            spaceshipManager = GameManager.Instance.SpaceshipManager;
            spaceshipManager.OnDoneServing += SendAway;
            spaceshipManager.OnActiveSpaceshipChange += Progress;
            spaceshipManager.OnEndQueue += (object sender, EventArgs e) =>
            {
                StopCoroutine(flickerEffect);
                staticFrit.Stop();
                flickerAudioSource.Stop();
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

                // ToggleFadeInOut out
                float duration = UnityEngine.Random.Range(minDuration, maxDuration);
                StartCoroutine(scan.ToggleFadeInOut(duration));
                StartCoroutine(plate.Fade(duration));
                yield return new WaitForSeconds(duration);

                // ToggleFadeInOut back in
                flickerAudioSource.Play();
                StartCoroutine(scan.ToggleFadeInOut(duration));
                StartCoroutine(plate.Fade(duration));
                yield return new WaitForSeconds(duration);
                flickerAudioSource.Stop();
            }
        }

        private void SendAway(object sender, Spaceship.Spaceship spaceship)
        {
            if (spaceship.Succeed)
            {
                spaceship.PlaySucceed();
            }
            else
            {
                spaceship.PlayFail();
            }
        }

        private void Progress(object sender, Spaceship.Spaceship spaceship)
        {
            int order = 0;
            foreach (Spaceship.Spaceship waitingSpaceship in spaceshipManager.WaitingQueue)
            {
                waitingSpaceship.MoveTo(GetPosition(order), GetScale(order));
                order++;
            }
            plate.SetText(spaceship.Plate);
            scan.SetTexture(spaceship.ScanTexture);
        }

        public Vector2 GetPosition(int order) => (Vector2)servingPosition.position + offset * order;

        public Vector3 GetScale(int order) => servingPosition.localScale + scaleFactor * order;
    }
}

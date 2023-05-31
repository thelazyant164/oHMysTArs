using Com.oHMysTArs.Pattern;
using Com.oHMysTArs.Queue;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.oHMysTArs.Spaceship
{
    public sealed class Spaceship : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private float moveSpeed = 1f;
        [SerializeField]
        private float scaleSpeed = 1f;
        [Space]

        [Header("Animation")]
        [SerializeField]
        private float fadeDuration = 1;
        [Space]

        [Header("SFX")]
        [SerializeField]
        private AudioClip succeedSFX;
        [SerializeField]
        private AudioClip failSFX;
        [SerializeField]
        private AudioClip moveSFX;
        [SerializeField]
        private AudioClip idleSFX;

        private AudioSource spaceshipAudio;
        private SpaceshipSO data;
        private QueueSystem state;
        private SpaceshipManager spaceshipManager;
        private Animator animator;
        private Image image;
        public string Name => data.name;
        public bool VIP => data.VIP;
        public Pattern.Pattern Pattern => data.Pattern;
        public int Attempts { get; private set; } = 0;
        public float ServeTime { get; private set; } = 0;
        public bool Succeed => state.CurrentQueueState is Succeed;
        public Texture2D ScanTexture => data.ScanTexture;
        public string Plate => data.Pattern.name;

        public void Init(SpaceshipSO so, Vector2 position, Vector3 scale)
        {
            data = so;
            image.SetTexture(so.Texture);
            transform.position = position;
            transform.localScale = scale;
            spaceshipManager = GameManager.Instance.SpaceshipManager;
            spaceshipManager.OnActiveSpaceshipChange += ProgressInQueue;
        }

        private void Awake()
        {
            spaceshipAudio = GetComponentInChildren<AudioSource>();
            state = GetComponentInChildren<QueueSystem>();
            state.SetState(new Waiting(state));
            image = GetComponentInChildren<Image>();
            animator = GetComponentInChildren<Animator>();
            animator.enabled = false;
        }

        private IEnumerator MoveTowards(Vector2 target)
        {
            float velocity = Vector2.Distance(transform.position, target) / moveSpeed;
            spaceshipAudio.Stop();
            spaceshipAudio.PlayOneShot(moveSFX);
            while (Vector2.Distance(transform.position, target) > Vector2.kEpsilon)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * velocity);
                yield return null;
            }
            spaceshipAudio.Stop();
        }

        private void ProgressInQueue(object sender, Spaceship spaceship) 
        {
            if (spaceship != this) return;
            state.SetState(new Serving(state));
            spaceshipAudio.Stop();
            spaceshipAudio.Play();
            spaceshipManager.OnActiveSpaceshipChange -= ProgressInQueue;
        }

        public void MoveTo(Vector2 position, Vector3 scale) 
        { 
            StartCoroutine(MoveTowards(position));
            StartCoroutine(gameObject.ScaleTo(scale, scaleSpeed));
        }

        public void Draw(object sender, bool match)
        {
            Attempts++;
            if (match) state.SetState(new Succeed(state));
        }

        public void Record(float time) => ServeTime = time;

        public void PlaySucceed()
        {
            animator.enabled = true;
            animator.SetTrigger("Succeed");
            StartCoroutine(image.ToggleFadeInOut(fadeDuration));
            spaceshipAudio.Stop();
            spaceshipAudio.PlayOneShot(succeedSFX);
        }

        public void PlayFail()
        {
            animator.enabled = true;
            animator.SetTrigger("Fail");
            StartCoroutine(image.ToggleFadeInOut(fadeDuration));
            spaceshipAudio.Stop();
            spaceshipAudio.PlayOneShot(failSFX);
        }

        public void StopServing() 
        { 
            state.SetState(new Waiting(state)); 
            ServeTime = 0;
        }
    }
}

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
        [SerializeField]
        private float velocity;
        [SerializeField]
        private float fadeDuration;
        private SpaceshipSO data;
        private QueueSystem state;
        private SpaceshipManager spaceshipManager;
        private Vector2 target;
        private Animator animator;
        private SpriteRenderer sprite;
        public bool VIP => data.VIP;
        public Pattern.Pattern Pattern => data.Pattern;
        public int Attempts { get; private set; } = 0;
        public float ServeTime { get; private set; } = 0;
        public bool Succeed => state.CurrentQueueState is Succeed;

        public void Init(SpaceshipSO so, Vector2 position)
        {
            data = so;
            transform.position = position;
        }

        private void Awake()
        {
            state = GetComponentInChildren<QueueSystem>();
            state.SetState(new Waiting(state));
            sprite = GetComponentInChildren<SpriteRenderer>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            spaceshipManager = GameManager.Instance.SpaceshipManager;
            spaceshipManager.OnActiveSpaceshipChange += ActiveSpaceshipChange;
        }

        private void FixedUpdate()
        {
            if (state.CurrentQueueState is not Waiting) return;
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * velocity);
        }

        private void ActiveSpaceshipChange(object sender, Spaceship spaceship) 
        {
            if (spaceship != this) return;
            state.SetState(new Serving(state));
            spaceshipManager.OnActiveSpaceshipChange -= ActiveSpaceshipChange;
        }

        public void MoveTo(Vector2 position) => target = position;

        public void Draw(object sender, bool match)
        {
            Attempts++;
            if (match) state.SetState(new Succeed(state));
        }

        public void Record(float time) => ServeTime = time;

        public void PlaySucceedAnimation()
        {
            animator.SetTrigger("Succeed");
            StartCoroutine(FadeOut());
        }

        public void PlayFailAnimation()
        {
            animator.SetTrigger("Fail");
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            float startingAlpha = sprite.material.color.a;
            float endAlpha = 0.0f;
            float timer = 0.0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float currentAlpha = Mathf.Lerp(startingAlpha, endAlpha, timer / fadeDuration);
                sprite.material.color = new Color(sprite.material.color.r, sprite.material.color.g, sprite.material.color.b, currentAlpha);
                yield return null;
            }
            sprite.enabled = false;
        }
    }
}

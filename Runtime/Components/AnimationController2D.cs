using ArashGh.Anima2D.Definitions;
using System.Collections.Generic;
using UnityEngine;

namespace ArashGh.Anima2D.Components
{
    public class AnimationController2D : MonoBehaviour
    {
        public List<Animation2DDefinition> animations;

        public bool PlayOnStart = false;
        public bool LoopCurrent = false;

        [HideInInspector]
        public int startAnimationIndex;
        public Animation2DDefinition StartAnimation;

        private Animation2DDefinition currentAnimation;
        private Animation2DDefinition nextAnimation;

        private bool isPlaying;
        private float lastFrameTime = 0.0f;
        private int currentFrame = -1;

        private SpriteRenderer spriteRenderer;

        public Animation2DDefinition this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                    return null;

                return animations.Find(animation => animation.name == name);
            }
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (StartAnimation)
            {
                SetAnimation(StartAnimation);

                if (PlayOnStart)
                {
                    PlayAnimation(StartAnimation, LoopCurrent);
                }
            }
        }

        public void Play(string name, bool loopAnimation)
        {
            PlayAnimation(this[name], loopAnimation);
        }

        public void PlayAfter(string name)
        {
            nextAnimation = this[name];
        }

        public void PlayAnimation(Animation2DDefinition animation, bool loopAnimation)
        {
            isPlaying = true;
            SetAnimation(animation);
            LoopCurrent = loopAnimation;
        }

        public void SetAnimation(Animation2DDefinition animation)
        {
            currentAnimation = animation.Instance as Animation2DDefinition;
            currentFrame = -1;
            spriteRenderer.sprite = currentAnimation[0];
        }

        private void PlayNext()
        {
            if (nextAnimation == null)
            {
                isPlaying = LoopCurrent;
                return;
            }

            currentAnimation = nextAnimation;
            nextAnimation = null;
        }

        private void Update()
        {
            if (!isPlaying)
                return;
            if (currentAnimation == null)
                return;
            if (currentAnimation.FrameCount <= 0)
                return;

            if (Time.time - lastFrameTime >= 1.0f / currentAnimation.FramesPerSecond)
            {
                currentFrame = (currentFrame + 1) % currentAnimation.FrameCount;

                spriteRenderer.sprite = CurrentFrame();

                lastFrameTime = Time.time;

                if (currentFrame == currentAnimation.FrameCount - 1)
                    PlayNext();
            }
        }

        public Sprite CurrentFrame()
        {
            return currentAnimation[currentFrame];
        }
    }
}

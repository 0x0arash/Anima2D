using ArashGh.Anima2D.Components;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArashGh.Anima2D.Definitions
{
    public class Animation2DDefinition : DefinitionBase
    {
        public int FramesPerSecond = 12;

        [SerializeField]
        [DisableIf("IsSpriteSheet")]
        private List<Sprite> frames = new List<Sprite>();

        public bool IsSpriteSheet = false;
        [SerializeField]
        [EnableIf("IsSpriteSheet")]
        private Sprite spriteSheet;

        public Sprite this[int index]
        {
            get
            {
                if (IsSpriteSheet)
                    throw new UnityException("Can't get frame from spritesheet.");

                return frames[index];
            }
        }

        public int FrameCount
        {
            get
            {
                if (IsSpriteSheet)
                    throw new UnityException("Can't get frame count if animation is a spritesheet.");

                return frames.Count;
            }
        }

        public Sprite SpriteSheet
        {
            get
            {
                return spriteSheet;
            }
        }
    }
}

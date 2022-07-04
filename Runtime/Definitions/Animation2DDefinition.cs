using ArashGh.Anima2D.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArashGh.Anima2D.Definitions
{
    public class Animation2DDefinition : DefinitionBase
    {
        public int FramesPerSecond = 12;

        [SerializeField]
        private List<Sprite> frames = new List<Sprite>();

        public Sprite this[int index]
        {
            get
            {
                return frames[index];
            }
        }

        public int FrameCount
        {
            get
            {
                return frames.Count;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArashGh.Anima2D.Definitions
{
    public class DefinitionBase : ScriptableObject
    {
        private DefinitionBase instance;
        public DefinitionBase Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = Instantiate(this);
                }

                return instance;
            }
        }
    }
}

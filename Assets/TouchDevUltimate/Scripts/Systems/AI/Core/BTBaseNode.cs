using System;
using UnityEngine;

namespace TouchDevUltimate.Systems.AI
{
    public abstract class BTBaseNode : ScriptableObject
    {
        public abstract BTNodeState Tick(GameObject owner);
    }
}
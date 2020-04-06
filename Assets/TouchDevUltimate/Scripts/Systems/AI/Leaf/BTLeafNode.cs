﻿using UnityEngine;

namespace TouchDevUltimate.Systems.AI
{
    public abstract class BTLeafNode : BTBaseNode
    {
        public override BTNodeState Tick(GameObject owner)
        {
            return Action();
        }

        protected abstract BTNodeState Action();
    }
}
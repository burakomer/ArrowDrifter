﻿using UnityEngine;

namespace TouchDevUltimate.Systems.AI
{
    public abstract class BTDecoratorNode : BTBaseNode
    {
        [Expandable] public BTBaseNode childNode;

        public override BTNodeState Tick(GameObject owner)
        {
            return DecoratedNode(owner);
        }

        protected abstract BTNodeState DecoratedNode(GameObject owner);
    }
}
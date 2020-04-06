using System;
using System.Collections.Generic;
using UnityEngine;

namespace TouchDevUltimate.Systems.AI
{
    /// <summary>
    /// Allows creating complex behaviours.
    /// </summary>
    [CreateAssetMenu(fileName = "New Behaviour Tree", menuName = "TouchRPGUltimate/AI/Behaviour Tree")]
    public class BehaviourTree : ScriptableObject
    {
        [Expandable] public List<BTBaseNode> nodes;

        public void Tick(GameObject owner)
        {
            foreach (BTBaseNode node in nodes)
            {
                node.Tick(owner);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TouchDevUltimate.Gameplay.Character
{
    public abstract class StatsManager<T> : MonoBehaviour where T : StatsCollection
    {
        public T stats;

        private void Start()
        {
        
        }
    }
}

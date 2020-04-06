using UnityEngine;

namespace TouchDevUltimate.Core.Input
{
    public abstract class TouchInput : MonoBehaviour
    {
        [Header("Properties")]
        public new string name;

        [SerializeField] protected bool interactive = true;

        public void SetInteractive(bool enabled)
        {
            interactive = enabled;
        }
    }
}
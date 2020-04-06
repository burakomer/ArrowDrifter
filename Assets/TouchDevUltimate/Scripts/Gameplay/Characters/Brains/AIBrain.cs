using TouchDevUltimate.Systems.AI;
using TouchDevUltimate.Core.Input;
using UnityEngine;

namespace TouchDevUltimate.Gameplay.Character.AI
{
    [RequireComponent(typeof(InputManager))]
    public class AIBrain : CharacterBrain
    {
        [Expandable] public BehaviourTree behaviourTree;

        protected override void SetInputManager()
        {
            inputManager = GetComponent<InputManager>();
        }

        protected override void UpdateBrain()
        {
            base.UpdateBrain();

            behaviourTree.Tick(gameObject);
        }
    }
}
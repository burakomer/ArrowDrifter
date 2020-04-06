using TouchDevUltimate.Core.Input;
using TouchDevUltimate.Core;
using UnityEngine;

namespace TouchDevUltimate.Gameplay.Character
{
    public class PlayerBrain : CharacterBrain
    {
        protected override void SetInputManager()
        {
            inputManager = GameManager.Instance.InputManager;
        }
    }
}
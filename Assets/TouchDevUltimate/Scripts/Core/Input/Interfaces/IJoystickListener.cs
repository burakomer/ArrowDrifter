using UnityEngine;

namespace TouchDevUltimate.Core.Input
{
    public interface IJoystickListener
    {
        void OnJoystick(string name, Vector2 direction, InputState state);
    }
}
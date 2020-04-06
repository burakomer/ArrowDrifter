using UnityEngine;

namespace TouchDevUltimate.Core.Input
{
    public delegate void ButtonEvent(string name, InputState state);
    public delegate void JoystickEvent(string name, Vector2 direction, InputState state);
    public delegate void GestureEvent(string name, GestureType type);
}
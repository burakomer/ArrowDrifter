using System;
using UnityEngine;

namespace TouchDevUltimate.Core.Input
{
    /// <summary>
    /// Receives input calls from appropriate sources and routes them to its listeners.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        private event ButtonEvent OnButton;
        private event JoystickEvent OnJoystick;
        private event GestureEvent OnGesture;

        public void AddListeners(IButtonListener buttonListener = null, IJoystickListener joystickListener = null,
            IGestureListener gestureListener = null)
        {
            if (buttonListener != null)
            {
                OnButton += buttonListener.OnButton;
            }

            if (joystickListener != null)
            {
                OnJoystick += joystickListener.OnJoystick;
            }

            if (gestureListener != null)
            {
                OnGesture += gestureListener.OnGesture;
            }
        }

        public void RemoveListeners(IButtonListener buttonListener = null, IJoystickListener joystickListener = null,
            IGestureListener gestureListener = null)
        {
            if (buttonListener != null)
            {
                OnButton -= buttonListener.OnButton;
            }

            if (joystickListener != null)
            {
                OnJoystick -= joystickListener.OnJoystick;
            }

            if (gestureListener != null)
            {
                OnGesture -= gestureListener.OnGesture;
            }
        }

        public void Button(string name, InputState state)
        {
            OnButton?.Invoke(name, state);
        }

        public void Joystick(string name, Vector2 direction, InputState state)
        {
            OnJoystick?.Invoke(name, direction, state);
        }

        public void Gesture(string name, GestureType type)
        {
            OnGesture?.Invoke(name, type);
        }
    }
}
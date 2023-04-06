using SharpDX.XInput;
using System;

namespace GamepadInputViewer.Model
{
    internal class GamepadXInput : GamepadBase
    {
        Controller controller;

        public GamepadXInput(Controller controller)
        {
            this.controller = controller;
        }
        public bool isConnected()
        {
            return controller.IsConnected;
        }

        public bool isTopButtonPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.Y) == GamepadButtonFlags.Y;
        }

        public bool isRightButtonPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.B) == GamepadButtonFlags.B;
        }

        public bool isBottomButtonPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.A) == GamepadButtonFlags.A;
        }

        public bool isLeftButtonPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.X) == GamepadButtonFlags.X;
        }

        public bool isStartButtonPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.Start) == GamepadButtonFlags.Start;
        }

        public bool isBackButtonPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.Back) == GamepadButtonFlags.Back;
        }

        public bool isLeftBumperPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.LeftShoulder) == GamepadButtonFlags.LeftShoulder;
        }

        public bool isRightBumperPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.RightShoulder) == GamepadButtonFlags.RightShoulder;
        }

        public bool isLeftJoystickPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.LeftThumb) == GamepadButtonFlags.LeftThumb;
        }

        public bool isRightJoystickPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.RightThumb) == GamepadButtonFlags.RightThumb;
        }

        public bool isDPadUpPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.DPadUp) == GamepadButtonFlags.DPadUp;
        }

        public bool isDPadRightPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.DPadRight) == GamepadButtonFlags.DPadRight;
        }

        public bool isDPadDownPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.DPadDown) == GamepadButtonFlags.DPadDown;
        }

        public bool isDPadLeftPressed()
        {
            return (getGamepadState().Buttons & GamepadButtonFlags.DPadLeft) == GamepadButtonFlags.DPadLeft;
        }

        public int getLeftTrigger()
        {
            return getGamepadState().LeftTrigger;
        }

        public int getRightTrigger()
        {
            return getGamepadState().RightTrigger;
        }

        public Tuple<int, int> getLeftJoystickAxes()
        {
            return new Tuple<int, int>(getGamepadState().LeftThumbX, getGamepadState().LeftThumbY);
        }

        public Tuple<int, int> getRightJoystickAxes()
        {
            return new Tuple<int, int>(getGamepadState().RightThumbX, getGamepadState().RightThumbY);
        }

        private Gamepad getGamepadState()
        {
            return controller.GetState().Gamepad;
        }
    }
}

using SharpDX.XInput;
using System;

namespace GamepadInputViewer.Model
{
    internal class GamepadXInput : GamepadBase
    {
        Controller? controller;
        int deviceId;

        public GamepadXInput(Controller controller)
        {
            this.controller = controller;
            if (controller != null)
            {
                deviceId = ((int)controller.UserIndex);
            }
        }
        public bool isConnected()
        {
            if (controller is null) return false;
            return controller.IsConnected;
        }

        public bool isTopButtonPressed()
        {
            if (getGamepadState().HasValue) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.Y) == GamepadButtonFlags.Y;
        }

        public bool isRightButtonPressed()
        {
            if (controller is null) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.B) == GamepadButtonFlags.B;
        }

        public bool isBottomButtonPressed()
        {
            if (controller is null) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.A) == GamepadButtonFlags.A;
        }

        public bool isLeftButtonPressed()
        {
            if (controller is null) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.X) == GamepadButtonFlags.X;
        }

        public bool isStartButtonPressed()
        {
            if (controller is null) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.Start) == GamepadButtonFlags.Start;
        }

        public bool isBackButtonPressed()
        {
            if (controller is null) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.Back) == GamepadButtonFlags.Back;
        }

        public bool isLeftBumperPressed()
        {
            if (controller is null) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.LeftShoulder) == GamepadButtonFlags.LeftShoulder;
        }

        public bool isRightBumperPressed()
        {
            if (controller is null) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.RightShoulder) == GamepadButtonFlags.RightShoulder;
        }

        public bool isLeftJoystickPressed()
        {
            if (controller is null) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.LeftThumb) == GamepadButtonFlags.LeftThumb;
        }

        public bool isRightJoystickPressed()
        {
            if (controller is null) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.RightThumb) == GamepadButtonFlags.RightThumb;
        }

        public bool isDPadUpPressed()
        {
            if (controller is null) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.DPadUp) == GamepadButtonFlags.DPadUp;
        }

        public bool isDPadRightPressed()
        {
            if (controller is null) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.DPadRight) == GamepadButtonFlags.DPadRight;
        }

        public bool isDPadDownPressed()
        {
            if (controller is null) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.DPadDown) == GamepadButtonFlags.DPadDown;
        }

        public bool isDPadLeftPressed()
        {
            if (controller is null) return false;
            return (getGamepadState().Value.Buttons & GamepadButtonFlags.DPadLeft) == GamepadButtonFlags.DPadLeft;
        }

        public int getLeftTrigger()
        {
            if (controller is null) return 0;
            return getGamepadState().Value.LeftTrigger;
        }

        public int getRightTrigger()
        {
            if (controller is null) return 0;
            return getGamepadState().Value.RightTrigger;
        }

        public Tuple<int, int> getLeftJoystickAxes()
        {
            if (controller is null) return new Tuple<int, int>(0,0);
            return new Tuple<int, int>(getGamepadState().Value.LeftThumbX, getGamepadState().Value.LeftThumbY);
        }

        public Tuple<int, int> getRightJoystickAxes()
        {
            if (controller is null) return new Tuple<int, int>(0, 0);
            return new Tuple<int, int>(getGamepadState().Value.RightThumbX, getGamepadState().Value.RightThumbY);
        }

        private Gamepad? getGamepadState()
        {
            if (controller is not null) return controller.GetState().Gamepad;
            return null;
        }

        public int getId()
        {
            return deviceId;
        }
        public void setId(int id)
        {
            deviceId = id;
        }
    }
}

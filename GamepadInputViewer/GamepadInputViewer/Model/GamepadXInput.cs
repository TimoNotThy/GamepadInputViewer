using SharpDX.XInput;
using System;

namespace GamepadInputViewer.Model
{
    internal class GamepadXInput : GamepadBase
    {
        Controller? controller;
        int deviceId;

        public GamepadXInput(Controller? controller)
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
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.Y) == GamepadButtonFlags.Y;
        }

        public bool isRightButtonPressed()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.B) == GamepadButtonFlags.B;
        }

        public bool isBottomButtonPressed()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.A) == GamepadButtonFlags.A;
        }

        public bool isLeftButtonPressed()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.X) == GamepadButtonFlags.X;
        }

        public bool isStartButtonPressed()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.Start) == GamepadButtonFlags.Start;
        }

        public bool isBackButtonPressed()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.Back) == GamepadButtonFlags.Back;
        }

        public bool isLeftBumperPressed()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.LeftShoulder) == GamepadButtonFlags.LeftShoulder;
        }

        public bool isRightBumperPressed()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.RightShoulder) == GamepadButtonFlags.RightShoulder;
        }

        public bool isLeftJoystickPressed()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.LeftThumb) == GamepadButtonFlags.LeftThumb;
        }

        public bool isRightJoystickPressed()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.RightThumb) == GamepadButtonFlags.RightThumb;
        }

        public bool isDPadUpPressed()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.DPadUp) == GamepadButtonFlags.DPadUp;
        }

        public bool isDPadRightPressed()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.DPadRight) == GamepadButtonFlags.DPadRight;
        }

        public bool isDPadDownPressed()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.DPadDown) == GamepadButtonFlags.DPadDown;
        }

        public bool isDPadLeftPressed()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return false;
            return (gamepad.Value.Buttons & GamepadButtonFlags.DPadLeft) == GamepadButtonFlags.DPadLeft;
        }

        public int getLeftTrigger()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return 0;
            return gamepad.Value.LeftTrigger;
        }

        public int getRightTrigger()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return 0;
            return gamepad.Value.RightTrigger;
        }

        public Tuple<int, int> getLeftJoystickAxes()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return new Tuple<int, int>(0,0);
            return new Tuple<int, int>(gamepad.Value.LeftThumbX, gamepad.Value.LeftThumbY);
        }

        public Tuple<int, int> getRightJoystickAxes()
        {
            var gamepad = getGamepadState();
            if (gamepad is null) return new Tuple<int, int>(0, 0);
            return new Tuple<int, int>(gamepad.Value.RightThumbX , gamepad.Value.RightThumbY);
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

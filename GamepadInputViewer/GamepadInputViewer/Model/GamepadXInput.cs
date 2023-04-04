using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadInputViewer.Model
{
    internal class GamePadXInput : GamepadBase
    {
        Controller controller = new Controller();

        public GamePadXInput(Controller controller)
        {
            this.controller = controller;
        }
        private bool isConnected()
        {
            return controller.IsConnected;
        }
        public bool isNothingPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.Y;
        }

        public bool isTopButtonPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.Y;
        }

        public bool isRightButtonPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.B;
        }

        public bool isBottomButtonPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.A;
        }

        public bool isLeftButtonPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.X;
        }

        public bool isStartButtonPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.Start;
        }

        public bool isBackButtonPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.Back;
        }

        public bool isLeftBumperPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.LeftShoulder;
        }

        public bool isRightBumperPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.RightShoulder;
        }

        public bool isLeftJoystickPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.LeftThumb;
        }

        public bool isRightJoystickPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.RightThumb;
        }

        public bool isDPadUpPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.DPadUp;
        }

        public bool isDPadRightPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.DPadRight;
        }

        public bool isDPadDownPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.DPadDown;
        }

        public bool isDPadLeftPressed()
        {
            return getGamepadState().Buttons == GamepadButtonFlags.DPadLeft;
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

        private SharpDX.XInput.Gamepad getGamepadState()
        {
            return controller.GetState().Gamepad;
        }
    }
}

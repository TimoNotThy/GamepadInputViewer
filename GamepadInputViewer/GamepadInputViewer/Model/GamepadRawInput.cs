using GamepadInputViewer.GamePadData;
using Linearstar.Windows.RawInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GamepadInputViewer.Model
{
    internal class GamepadRawInput : IGamepad
    {
        GamepadInputData gamepadInputData;
        RawInputDevice? rawInputDevice;
        int deviceId;

        public GamepadRawInput(RawInputDevice? rawInputDevice, GamepadInputData gamepadInputData)
        {
            this.gamepadInputData = gamepadInputData;
            this.rawInputDevice = rawInputDevice;
                deviceId = 0;
        }

        public bool isTopButtonPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.Y) == RawInputButtonFlags.Y;
        }

        public bool isRightButtonPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.B) == RawInputButtonFlags.B;
        }

        public bool isBottomButtonPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.A) == RawInputButtonFlags.A;
        }

        public bool isLeftButtonPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.X) == RawInputButtonFlags.X;
        }

        public bool isStartButtonPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.Start) == RawInputButtonFlags.Start;
        }

        public bool isBackButtonPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.Back) == RawInputButtonFlags.Back;
        }

        public bool isLeftBumperPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.LeftBumper) == RawInputButtonFlags.LeftBumper;
        }

        public bool isRightBumperPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.RightBumper) == RawInputButtonFlags.RightBumper;
        }

        public bool isLeftJoystickPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.LeftThumb) == RawInputButtonFlags.LeftThumb;
        }

        public bool isRightJoystickPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.RightThumb) == RawInputButtonFlags.RightThumb;
        }

        public bool isDPadUpPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.DPadUp) == RawInputButtonFlags.DPadUp;
        }

        public bool isDPadRightPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.DPadRight) == RawInputButtonFlags.DPadRight;
        }

        public bool isDPadDownPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.DPadDown) == RawInputButtonFlags.DPadDown;
        }

        public bool isDPadLeftPressed()
        {
            return (gamepadInputData.button & RawInputButtonFlags.DPadLeft) == RawInputButtonFlags.DPadLeft;
        }

        public int getLeftTrigger()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.LeftTrigger) == RawInputButtonFlags.LeftTrigger) return 255;
            return 0;
        }

        public int getRightTrigger()
        {
            
            if ((gamepadInputData.button & RawInputButtonFlags.RightTrigger) == RawInputButtonFlags.RightTrigger) return 255;
            return 0;
        }

        public Tuple<int, int> getLeftJoystickAxes()
        {
            return new Tuple<int, int>(gamepadInputData.x*256, -gamepadInputData.y*256);
        }

        public Tuple<int, int> getRightJoystickAxes()
        {
            return new Tuple<int, int>(gamepadInputData.z*256, -gamepadInputData.Rx*256);
        }

        public Tuple<double, double> getGyroscopeAxes()
        {
            return new Tuple<double, double>(Math.Round((double)gamepadInputData.Ry /128,3), Math.Round((double)-gamepadInputData.Rz / 128, 3));
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

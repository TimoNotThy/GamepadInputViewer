using GamepadInputViewer.GamePadData;
using Linearstar.Windows.RawInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GamepadInputViewer.Model
{
    internal class GamepadRawInput : GamepadBase
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

        public bool isConnected()
        {
            return true ;
        }

        public bool isTopButtonPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.Y) == RawInputButtonFlags.Y) return true;
            return false;
        }

        public bool isRightButtonPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.B) == RawInputButtonFlags.B) return true;
            return false;
        }

        public bool isBottomButtonPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.A) == RawInputButtonFlags.A) return true;
            return false;
        }

        public bool isLeftButtonPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.X) == RawInputButtonFlags.X) return true;
            return false;
        }

        public bool isStartButtonPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.Start) == RawInputButtonFlags.Start) return true;
            return false;
        }

        public bool isBackButtonPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.Back) == RawInputButtonFlags.Back) return true;
            return false;
        }

        public bool isLeftBumperPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.LeftBumper) == RawInputButtonFlags.LeftBumper) return true;
            return false;
        }

        public bool isRightBumperPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.RightBumper) == RawInputButtonFlags.RightBumper) return true;
            return false;
        }

        public bool isLeftJoystickPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.LeftThumb) == RawInputButtonFlags.LeftThumb) return true;
            return false;
        }

        public bool isRightJoystickPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.RightThumb) == RawInputButtonFlags.RightThumb) return true;
            return false;
        }

        public bool isDPadUpPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.DPadUp) == RawInputButtonFlags.DPadUp) return true;
            return false;
        }

        public bool isDPadRightPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.DPadRight) == RawInputButtonFlags.DPadRight) return true;
            return false;
        }

        public bool isDPadDownPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.DPadDown) == RawInputButtonFlags.DPadDown) return true;
            return false;
        }

        public bool isDPadLeftPressed()
        {
            if ((gamepadInputData.button & RawInputButtonFlags.DPadLeft) == RawInputButtonFlags.DPadLeft) return true;
            return false;
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
            return new Tuple<int, int>(gamepadInputData.z*256, gamepadInputData.Rx*256);
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

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
            return true;
        }

        public bool isTopButtonPressed()
        {
            Debug.WriteLine(gamepadInputData.Ry + " " + gamepadInputData.Rz);
            if ((gamepadInputData.button1 & RawInputButton1Flags.Y) == RawInputButton1Flags.Y) return true;
            return false;
        }

        public bool isRightButtonPressed()
        {
            if ((gamepadInputData.button1 & RawInputButton1Flags.B) == RawInputButton1Flags.B) return true;
            return false;
        }

        public bool isBottomButtonPressed()
        {
            if ((gamepadInputData.button1 & RawInputButton1Flags.A) == RawInputButton1Flags.A) return true;
            return false;
        }

        public bool isLeftButtonPressed()
        {
            if ((gamepadInputData.button1 & RawInputButton1Flags.X) == RawInputButton1Flags.X) return true;
            return false;
        }

        public bool isStartButtonPressed()
        {
            if ((gamepadInputData.button1 & RawInputButton1Flags.Start) == RawInputButton1Flags.Start) return true;
            return false;
        }

        public bool isBackButtonPressed()
        {
            if ((gamepadInputData.button1 & RawInputButton1Flags.Back) == RawInputButton1Flags.Back) return true;
            return false;
        }

        public bool isLeftBumperPressed()
        {
            if ((gamepadInputData.button1 & RawInputButton1Flags.LeftBumper) == RawInputButton1Flags.LeftBumper) return true;
            return false;
        }

        public bool isRightBumperPressed()
        {
            if ((gamepadInputData.button1 & RawInputButton1Flags.RightBumper) == RawInputButton1Flags.RightBumper) return true;
            return false;
        }

        public bool isLeftJoystickPressed()
        {
            if ((gamepadInputData.button2 & RawInputButton2Flags.LeftThumb) == RawInputButton2Flags.LeftThumb) return true;
            return false;
        }

        public bool isRightJoystickPressed()
        {
            if ((gamepadInputData.button2 & RawInputButton2Flags.RightThumb) == RawInputButton2Flags.RightThumb) return true;
            return false;
        }

        public bool isDPadUpPressed()
        {
            if ((gamepadInputData.button2 & RawInputButton2Flags.DPadUp) == RawInputButton2Flags.DPadUp) return true;
            return false;
        }

        public bool isDPadRightPressed()
        {
            if ((gamepadInputData.button2 & RawInputButton2Flags.DPadRight) == RawInputButton2Flags.DPadRight) return true;
            return false;
        }

        public bool isDPadDownPressed()
        {
            if ((gamepadInputData.button2 & RawInputButton2Flags.DPadDown) == RawInputButton2Flags.DPadDown) return true;
            return false;
        }

        public bool isDPadLeftPressed()
        {
            if ((gamepadInputData.button2 & RawInputButton2Flags.DPadLeft) == RawInputButton2Flags.DPadLeft) return true;
            return false;
        }

        public int getLeftTrigger()
        {
            if ((gamepadInputData.button2 & RawInputButton2Flags.LeftTrigger) == RawInputButton2Flags.LeftTrigger) return 255;
            return 0;
        }

        public int getRightTrigger()
        {
            
            if ((gamepadInputData.button2 & RawInputButton2Flags.RightTrigger) == RawInputButton2Flags.RightTrigger) return 255;
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

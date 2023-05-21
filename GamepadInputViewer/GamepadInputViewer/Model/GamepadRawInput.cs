using Linearstar.Windows.RawInput;
using System;
using System.Collections.Generic;

namespace GamepadInputViewer.Model
{
    internal class GamepadRawInput : GamepadBase
    {
        List<sbyte> rawInputHidData;
        RawInputDevice? rawInputDevice;
        int deviceId;

        public GamepadRawInput(RawInputDevice? rawInputDevice, List<sbyte> rawInputHidData)
        {
            this.rawInputHidData = rawInputHidData;
            this.rawInputDevice = rawInputDevice;
            if (rawInputHidData is not null)
            {
                deviceId = 0;
            }
        }

        public bool isConnected()
        {
            return true;
        }

        public bool isTopButtonPressed()
        {

            if (rawInputHidData[19] == 8)
            {
                return true;
            }
            return false;
        }

        public bool isRightButtonPressed()
        {
            if (rawInputHidData[19] == 2)
            {
                return true;
            }
            return false;
        }

        public bool isBottomButtonPressed()
        {
            if (rawInputHidData[19] == 1)
            {
                return true;
            }
            return false;
        }

        public bool isLeftButtonPressed()
        {
            if (rawInputHidData[19] == 4)
            {
                return true;
            }
            return false;
        }

        public bool isStartButtonPressed()
        {
            return false;
        }

        public bool isBackButtonPressed()
        {
            return false;
        }

        public bool isLeftBumperPressed()
        {
            return false;
        }

        public bool isRightBumperPressed()
        {
            return false;
        }

        public bool isLeftJoystickPressed()
        {
            return false;
        }

        public bool isRightJoystickPressed()
        {
            return false;
        }

        public bool isDPadUpPressed()
        {
            return false;
        }

        public bool isDPadRightPressed()
        {
            return false;
        }

        public bool isDPadDownPressed()
        {
            return false;
        }

        public bool isDPadLeftPressed()
        {
            return false;
        }

        public int getLeftTrigger()
        {
            return 1;
        }

        public int getRightTrigger()
        {
            return 1;
        }

        public Tuple<int, int> getLeftJoystickAxes()
        {
            return new Tuple<int, int>(0,0);
        }

        public Tuple<int, int> getRightJoystickAxes()
        {
            return new Tuple<int, int>(0, 0);
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

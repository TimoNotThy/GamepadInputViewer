using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadInputViewer.Model
{
    internal interface GamepadBase
    {
        public bool isTopButtonPressed();
        public bool isRightButtonPressed();
        public bool isBottomButtonPressed();
        public bool isLeftButtonPressed();
        public bool isStartButtonPressed();
        public bool isBackButtonPressed();
        public bool isLeftBumperPressed();
        public bool isRightBumperPressed();
        public bool isLeftJoystickPressed();
        public bool isRightJoystickPressed();
        public bool isDPadUpPressed();
        public bool isDPadRightPressed();
        public bool isDPadDownPressed();
        public bool isDPadLeftPressed();
        public int getLeftTrigger();
        public int getRightTrigger();
        public Tuple<int, int> getLeftJoystickAxes();
        public Tuple<int, int> getRightJoystickAxes();

    }
}

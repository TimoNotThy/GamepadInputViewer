using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadInputViewer.Model
{
    internal class GamepadDirectInput : GamepadBase
    {
        Joystick controller;

        public GamepadDirectInput(Joystick controller)
        {
            this.controller = controller;
            controller.Properties.BufferSize = 128;
            controller.Acquire();
        }

        public bool isConnected()
        {
            return true;
        }

        public bool isTopButtonPressed()
        {
            return controller.GetCurrentState().Buttons[3];
        }
        public bool isRightButtonPressed()
        {
            return controller.GetCurrentState().Buttons[1];
        }
        public bool isBottomButtonPressed()
        {
            return controller.GetCurrentState().Buttons[0];
        }
        public bool isLeftButtonPressed()
        {
            return controller.GetCurrentState().Buttons[2];
        }
        public bool isStartButtonPressed()
        {
            return controller.GetCurrentState().Buttons[7];
        }
        public bool isBackButtonPressed()
        {
            return controller.GetCurrentState().Buttons[6];
        }
        public bool isLeftBumperPressed()
        {
            return controller.GetCurrentState().Buttons[4];
        }
        public bool isRightBumperPressed()
        {
            return controller.GetCurrentState().Buttons[5];
        }
        public bool isLeftJoystickPressed()
        {
            return controller.GetCurrentState().Buttons[8];
        }
        public bool isRightJoystickPressed()
        {
            return controller.GetCurrentState().Buttons[9];
        }
        public bool isDPadUpPressed()
        {
            int? value = -1;
            if(controller.GetCurrentState().PointOfViewControllers.GetValue(0) is not null) {
                value = (int?)controller.GetCurrentState().PointOfViewControllers.GetValue(0);
            }
            return value == 0 || value == 31500 || value == 4500;
        }
        public bool isDPadRightPressed()
        {
            int? value = -1;
            if (controller.GetCurrentState().PointOfViewControllers.GetValue(0) is not null)
            {
                value = (int?)controller.GetCurrentState().PointOfViewControllers.GetValue(0);
            }
            return value == 4500 || value == 9000 || value == 13500;
        }
        public bool isDPadDownPressed()
        {
            int? value = -1;
            if (controller.GetCurrentState().PointOfViewControllers.GetValue(0) is not null)
            {
                value = (int?)controller.GetCurrentState().PointOfViewControllers.GetValue(0);
            }
            return value == 13500 || value == 18000 || value == 22500;
        }
        public bool isDPadLeftPressed()
        {
            int? value = -1;
            if (controller.GetCurrentState().PointOfViewControllers.GetValue(0) is not null)
            {
                value = (int?)controller.GetCurrentState().PointOfViewControllers.GetValue(0);
            }
            return value == 22500 || value == 27000 || value == 31500;
        }
        public int getLeftTrigger()
        {
            if(controller.GetCurrentState().Z > 32767)
            {
                return (controller.GetCurrentState().Z - 32767) / 256;
            }
            else
            {
                return 0;
            }
        }
        public int getRightTrigger()
        {
            if (controller.GetCurrentState().Z < 32767)
            {
                return Math.Abs(((controller.GetCurrentState().Z) / 256) - 128);
            }
            else
            {
                return 0;
            }
            
        }
        public Tuple<int, int> getLeftJoystickAxes()
        {
            int xAxis = controller.GetCurrentState().X;
            int yAxis = controller.GetCurrentState().Y;
                xAxis -= 32767;
                yAxis -= 32767;
            return new Tuple<int, int>(xAxis, -yAxis);
        }
        public Tuple<int, int> getRightJoystickAxes()
        {
            int xAxis = controller.GetCurrentState().RotationX;
            int yAxis = controller.GetCurrentState().RotationY;
            xAxis -= 32767;
            yAxis -= 32767;
            return new Tuple<int, int>(xAxis, -yAxis);
        }

    }
}

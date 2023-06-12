using SharpDX.DirectInput;
using System;
using System.Diagnostics;

namespace GamepadInputViewer.Model
{
    internal class GamepadDirectInput : IGamepad
    {
        Joystick? controller;
        int deviceId;

        public GamepadDirectInput(Joystick? controller)
        {
            this.controller = controller;

            if (controller is not null)
            {
                controller.Properties.BufferSize = 128;
                controller.Acquire();
                deviceId = controller.Properties.JoystickId;
            }
        }

        public bool isTopButtonPressed()
        {
            if (controller is not null) return controller.GetCurrentState().Buttons[3];
            else return false; 
        }
        public bool isRightButtonPressed()
        {
            if (controller is not null) return controller.GetCurrentState().Buttons[1];
            else return false;
        }
        public bool isBottomButtonPressed()
        {
            if (controller is not null) return controller.GetCurrentState().Buttons[0];
            else return false;
        }
        public bool isLeftButtonPressed()
        {
            if (controller is not null) return controller.GetCurrentState().Buttons[2];
            else return false;
        }
        public bool isStartButtonPressed()
        {
            if (controller is not null) return controller.GetCurrentState().Buttons[7];
            else return false;
        }
        public bool isBackButtonPressed()
        {
            if (controller is not null) return controller.GetCurrentState().Buttons[6];
            else return false;
        }
        public bool isLeftBumperPressed()
        {
            if (controller is not null) return controller.GetCurrentState().Buttons[4];
            else return false;
        }
        public bool isRightBumperPressed()
        {
            if (controller is not null) return controller.GetCurrentState().Buttons[5];
            else return false;
        }
        public bool isLeftJoystickPressed()
        {
            if (controller is not null) return controller.GetCurrentState().Buttons[8];
            else return false;
        }
        public bool isRightJoystickPressed()
        {
            if (controller is not null) return controller.GetCurrentState().Buttons[9];
            else return false;
        }
        public bool isDPadUpPressed()
        {
            if (controller is null) return false;
            int? value = -1;
            if (controller.GetCurrentState().PointOfViewControllers.GetValue(0) is not null)
            {
                value = (int?)controller.GetCurrentState().PointOfViewControllers.GetValue(0);
            }
            return value == 0 || value == 31500 || value == 4500;
        }
        public bool isDPadRightPressed()
        {
            if (controller is null) return false;
            int? value = -1;
            if (controller.GetCurrentState().PointOfViewControllers.GetValue(0) is not null)
            {
                value = (int?)controller.GetCurrentState().PointOfViewControllers.GetValue(0);
            }
            return value == 4500 || value == 9000 || value == 13500;
        }
        public bool isDPadDownPressed()
        {
            if (controller is null) return false;
            int? value = -1;
            if (controller.GetCurrentState().PointOfViewControllers.GetValue(0) is not null)
            {
                value = (int?)controller.GetCurrentState().PointOfViewControllers.GetValue(0);
            }
            return value == 13500 || value == 18000 || value == 22500;
        }
        public bool isDPadLeftPressed()
        {
            if (controller is null) return false;
            int? value = -1;
            if (controller.GetCurrentState().PointOfViewControllers.GetValue(0) is not null)
            {
                value = (int?)controller.GetCurrentState().PointOfViewControllers.GetValue(0);
            }
            return value == 22500 || value == 27000 || value == 31500;
        }
        public int getLeftTrigger()
        {
            if (controller is null) return 0;
            if (controller.GetCurrentState().Z > 32767)
            {
                return (controller.GetCurrentState().Z - 32767) / 256;
            }
                return 0;
        }
        public int getRightTrigger()
        {
            if (controller is null) return 0;
            if (controller.GetCurrentState().Z < 32767)
            {
                return Math.Abs(((controller.GetCurrentState().Z) / 256) - 128);
            }
                return 0;

        }
        public Tuple<int, int> getLeftJoystickAxes()
        {
            if (controller is null) return new Tuple<int, int>(0, 0);
            int xAxis = controller.GetCurrentState().X;
            int yAxis = controller.GetCurrentState().Y;
            xAxis -= 32767;
            yAxis -= 32767;
            return new Tuple<int, int>(xAxis, -yAxis);
        }
        public Tuple<int, int> getRightJoystickAxes()
        {
            if (controller is null) return new Tuple<int, int>(0, 0);
            int xAxis = controller.GetCurrentState().RotationX;
            int yAxis = controller.GetCurrentState().RotationY;
            xAxis -= 32767;
            yAxis -= 32767;
            return new Tuple<int, int>(xAxis, -yAxis);
        }

        public Tuple<double, double> getGyroscopeAxes()
        {
            return new Tuple<double, double>(0, 0);
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
